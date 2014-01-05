# 
# Creates console shortcuts to all installed IronPython versions. 
# The shortcuts include tab-completion and colors. 
# 
# NOTE: Must be run with Administrator privileges 
# 

$shell = new-object -com "WScript.Shell" 
$ipproducts = get-wmiobject Win32_Product | Where-Object { $_.Name -like "IronPython*" } 
$startmenupath = $shell.SpecialFolders.Item("AllUsersStartMenu") 
$workingdir  = $shell.SpecialFolders.Item("MyDocuments") 

foreach ($p in $ipproducts) 
{  
    $link_folder =  $startmenupath 
    $ipy_filename = join-path $p.InstallLocation "ipy.exe" 
    $shortcut_path = join-path $startmenupath (join-path "Programs" $p.Name) 
    if ( test-path $ipy_filename ) 
    { 
        $link_filename = join-path $shortcut_path "IronPython Console (enhanced).lnk" 
        $lnk = $shell.CreateShortcut($link_filename) 
        $lnk.TargetPath = $p.InstallLocation + "ipy.exe" 
        $lnk.Arguments = "-X:ColorfulConsole -X:TabCompletion" 
        $lnk.WorkingDirectory = $workingdir 
        $lnk.Save()    

    } 
}

