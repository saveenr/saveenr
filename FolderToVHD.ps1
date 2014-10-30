Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

function folder_to_vhd( $source_folder, $dest_vhd )
{
    if (!(Test-Path $source_folder))
    {
        Write-Host ERROR: Source Folder Does Not exist
        exit
    }

    $dest_vhd_parent = Split-Path $dest_vhd -Parent
    if (!(Test-Path $dest_vhd_parent))
    {
        Write-Host ERROR: Destination Folder Does Not exist
        exit
    }

    $overwrite = $true

    if (Test-Path $dest_vhd)
    {
        Write-Host Dest VHD already exists
        if ($overwrite)
        {
            Write-Host Attempting to delete
            Remove-Item $dest_vhd

            if (Test-Path $dest_vhd)
            {
                Write-Host Could not delete VHD
            }
        }
        else
        {
            Write-Host ERROR: Dest VHD already exists
            exit
        }
    }

    # Find out the exact number of bytes of the source folder
    # NOTE: sure how if accounts for hidden items

    $stats = Get-ChildItem $source_folder | Measure-Object -Sum Length
    $source_size_in_bytes = $stats.Sum

    # Calcualte the VHD size in bytes
    # NOTE: incorporating some padding and to align the size on some byte boundary
    $blocksize = [int32] 512
    $padding = 100MB
    $source_size_in_bytes_adjusted = $source_size_in_bytes + $padding
    $source_size_in_bytes_adjusted = $source_size_in_bytes_adjusted/$blocksize
    $source_size_in_bytes_adjusted = [int64] $source_size_in_bytes_adjusted
    $source_size_in_bytes_adjusted = $source_size_in_bytes_adjusted*$blocksize

    # Verify that handling the byte boundary was done correctly
    $remainder = $source_size_in_bytes_adjusted % $blocksize
    if ($remainder -ne 0)
    {
        Write-Host ERROR: Did not align vhd size on $blocksize boundary
        exit
    }

    $vhd = New-VHD -Path $dest_vhd -Dynamic -SizeBytes $source_size_in_bytes_adjusted
    $vdrive = Mount-VHD -Path $dest_vhd -Passthru
    $disk_number = $vdrive.Number
    $disk = Initialize-Disk -Number $disk_number -PartitionStyle MBR -PassThru
    $partition = New-Partition -InputObject $disk -UseMaximumSize -AssignDriveLetter:$False -MbrType IFS 
    $partition_number = $partition.PartitionNumber 

    # Format the Partition
    Write-Host Formatting Partition $partition_number on Disk $disk_number
    $object_1 = Format-Volume -Confirm:$false -FileSystem NTFS -force -Partition $partition

    # Assign a Drive letter
    $x4 = Add-PartitionAccessPath -PartitionNumber $partition_number -AssignDriveLetter -DiskNumber $disk_number -PassThru 
    
    # Find the volume for that partition (primarily so we know which driveletter was assigned)
    $vol = Get-Volume -Partition $partition   
    Write-Host VHD Mounted as Drive $vol.DriveLetter

    # Calculate the destination path within the VHD
    # This will be the name of the source folder
    $dest_root_path = $vol.DriveLetter + ":\"
    $dest_foldername = Split-Path $source_folder -Leaf
    $dest_path = Join-Path $dest_root_path $dest_foldername

    # Mirror the contents using the very efficient robocopy tool    
    #robocopy $source_folder $dest_path /mir

    # Now unmount that disk
    Write-Host Detaching the VHD
    Dismount-VHD -DiskNumber $disk_number
}



$src_fldr = "D:\workfolder\canon-eos-20d"
$dest_fldr =  "D:\"
$basename = Split-Path $src_fldr -Leaf
$dest_vhd = Join-Path $dest_fldr ($basename + ".vhd" )

folder_to_vhd $src_fldr $dest_vhd