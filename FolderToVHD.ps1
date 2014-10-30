$source_folder = "D:\workfolder\canon-eos-20d"
$dest_vhd = "D:\canon-eos-20d.vhd"

if (Test-Path $dest_vhd)
{
    Write-Host Dest VHD already exists
    exit
}


$stats = Get-ChildItem $source_folder | Measure-Object -Sum Length

$source_size_in_bytes = $stats.Sum
$source_size_in_bytes_adjusted = ([int64] (($source_size_in_bytes + 100MB)/512))*512
$vhd = New-VHD -Path $dest_vhd -Dynamic -SizeBytes $source_size_in_bytes_adjusted
$vdrive = Mount-VHD -Path "D:\canon-eos-20d.vhd" -Passthru
$disknumber = $vdrive.Number
$x1 = Initialize-Disk -Number $disknumber -PartitionStyle MBR -PassThru
$x2 = New-Partition -InputObject $x1 -UseMaximumSize -AssignDriveLetter:$False -MbrType IFS 
$x3 = Format-Volume -Confirm:$false -FileSystem NTFS -force -Partition $x2
$partitions = Get-Partition -Volume $x3
$part0 = $partitions[0]
$x4 = Add-PartitionAccessPath -PartitionNumber $part0.PartitionNumber -AssignDriveLetter -DiskNumber $disknumber -PassThru 
$vol = Get-Volume -Partition $part0

Write-Host Drive $vol.DriveLetter Created


$dest_path = $vol.DriveLetter + ":\"

robocopy $source_folder $dest_path /mir
