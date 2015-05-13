# FolderToVHD.ps1
#
# Description: Creates a VHD From a Folder
# Author: Saveen Reddy
#

param(
  [Parameter(Mandatory=$true)] [string]$Folder,
  [Parameter(Mandatory=$true)] [string]$VHD
)

Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(`
    [Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You do not have Administrator rights to run this script!`nPlease re-run this script as an Administrator!"
    Break
}

function folder_to_vhd( $source_folder, $dest_vhd )
{
    Write-Host FROM $source_folder
    Write-Host   TO $dest_vhd
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
    # NOTE: -Force will included hidden files

    $childitems = dir $source_folder -Force -File -Recurse
    if ($childitems -eq $null)
    {
        Write-Error "No Items in Source Folder"
        exit
    }

    #Write-Host $childitems.GetType()

    $stats = $childitems  | Measure-Object -Sum Length
    $source_size_in_bytes = $stats.Sum

    Write-Host Total size of folder on disk $source_size_in_bytes

    # Calcualte the VHD size in bytes
    # NOTE: incorporating some padding and to align the size on some byte boundary
    $blocksize = [int32] 512
    $padding = [int64] ( ($source_size_in_bytes * 0.1) + 100MB ) # Make the drive a little bigger than we need
    Write-Host Extra Padding in bytes = $padding
    $source_size_in_bytes_adjusted = [int64] ($source_size_in_bytes + $padding)
    $source_size_in_bytes_adjusted = ( [int64]($source_size_in_bytes_adjusted/$blocksize))*$blocksize

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

    # Create the Partition
    Write-Host Creating the Partition
    $partition = New-Partition -InputObject $disk -UseMaximumSize -AssignDriveLetter -MbrType IFS 
    $partition_number = $partition.PartitionNumber 

    # Format the Partition
    Write-Host Formatting Partition $partition_number on Disk $disk_number
    $object_1 = Format-Volume -Confirm:$false -FileSystem NTFS -force -Partition $partition
    
    # Find the volume for that partition (primarily so we know which driveletter was assigned)
    $vol = Get-Volume -Partition $partition   
    $drive_letter = $vol.DriveLetter
    $drive_name = $drive_letter + ":"
    Write-Host VHD Mounted as Drive $drive_letter

    # Calculate the destination path within the VHD
    # This will be the name of the source folder
    $dest_root_path = $drive_letter + ":\"
    $dest_foldername = Split-Path $source_folder -Leaf
    $dest_path = Join-Path $dest_root_path $dest_foldername

    # Mirror the contents using the very efficient robocopy tool    
    robocopy $source_folder $dest_path /mir

    # Now unmount that disk
    Write-Host Detaching the VHD Drive $drive_name
    Dismount-VHD -DiskNumber $disk_number
}


folder_to_vhd $Folder $VHD
