#
# Modulfe: CodePackage
#
# Cmdlets to help distribute code:
# - Create ZIP files
# - Remove VIsual Studio Source Code bindings
# - Create Simple MSI Installers to install a folder 
# 		- into Program Files
#		- into Current User's Powershell Modules folder
# - Copy a folder of code without the the unecessary files

# WORKITEMS
# - Implement cmdlet Get-SolutionFilesInFolder -Recursive
# - Implement cmdlet Get-ProjectFilesInFolder -Recursive
# - Create Distributions including Source, Debug/Release binaries
# - Add Start Menu shortcut to MSI Installer
# - Add Machine-level install for PowerShell Modules
# - Log output of WIX binaries
# - Additional Unit Tests
# - CmdLet to Create GUIDs for Installers

# HISTORY
# 2013-02-23 - Added cmdlet to install this Module
# 2013-02-23 - Now exclude .hg when performing code copy
# 2013-02-23 - Version 1.0

Set-StrictMode -Version 2 
$ErrorActionPreference = "Stop"

function Create-FolderIfNotExists
{
	param
	(
		[parameter(Mandatory=$true)] [string] $Folder , 
		[parameter(Mandatory=$true)] [string] $Description 
	)
	process
	{
		if (!(test-path $Folder))
		{
			Write-Verbose "Creating $Description"
            New-Item $Folder -ItemType directory | Out-Null
		}
	}
}

function Remove-FolderIfExists
{
	param
	(
		[parameter(Mandatory=$true)] [string] $Folder , 
		[parameter(Mandatory=$true)] [string] $Description 
	)
	process
	{
		if ( test-path $Folder)
		{
			Write-Verbose $Description
    		Remove-Item $Folder -Recurse
		}
	}
}

function Remove-FileIfExists
{
	param
	(
		[parameter(Mandatory=$true)] [string] $Filename , 
		[parameter(Mandatory=$true)] [string] $Description 
	)
	process
	{
		if ( test-path $Filename)
		{
			Write-Verbose $Description
    		Remove-Item $Filename -Recurse
		}
	}
}

function Copy-CodeFolder
{
	param
	(
		[parameter(Mandatory=$true)] [string] $SourceFolder , 
		[parameter(Mandatory=$true)] [string] $OutputFolder
	)
	process
	{
	
		# ---------------------------------
		# COPY FILES TO THE STAGING FOLDER
		# Remove the read-only flag with /A-:R
		# Exclude Files with /XF option
		#  *.suo 
		#  *.user 
		#  *.vssscc 
		#  *.vspscc 
		# Exclude folders with /XD option
		#  bin
		#  obj
		#  _Resharper

		# Control verbosity 
		#  Don't show the names of files /NFL
		#  Don't show the names of directories /NDL
		&robocopy $SourceFolder $OutputFolder /MIR /A-:R /XF *.suo /XF *.user /XF *.vssscc /XF *.vspscc /XF *.ignore /XF *.temp /XF *.tmp /NFL /NDL /XD bin /XD obj /XD _ReSharper* /XD .hg
	}
}



function UnbindVSSourceControl
{
	param
	(
		[parameter(Mandatory=$true)] [string] $Folder
	)
	process
	{
		if (!(test-path $Folder))
		{
			Write-Host $Folder does not exist
			return;
		}

		function RemoveSCCElementsAttributes($el)
		{
			if ($el.Name.LocalName.StartsWith("Scc"))
			{
				# the the current element starts with Scc
				# Prune it and its children from the DOM
				$el.Remove();
				return;
			}
			else
			{
				# The current elemenent does not start with Scc
				# delete and Scc attributes it may have
				foreach ($attr in $el.Attributes())
				{
					if ($attr.Name.LocalName.StartsWith("Scc"))
					{
						$attr.Remove();
					}
				}

				# Check the children for any SCC Elements or attributes
				foreach ($child in $el.Elements())
				{
					RemoveSCCElementsAttributes($child);
				}
			}
		}

		Write-Verbose Unbinding SLN files from Source Control
		Write-Verbose $Folder
		$slnfiles = Get-ChildItem $Folder *.sln -Recurse

		foreach ($slnfile in $slnfiles) 
		{
			Write-Verbose $slnfile
			$insection = $false
			write-host $slnfile
			$input_lines = get-content $slnfile.FullName
			$output_lines = new-object 'System.Collections.Generic.List[string]'

			foreach ($line in $input_lines) 
			{
				$line_trimmed = $line.Trim()

				if ($line_trimmed.StartsWith("GlobalSection(SourceCodeControl)") -Or $line_trimmed.StartsWith("GlobalSection(TeamFoundationVersionControl)"))
				{
					$insection = $true	
					# do not copy this line to output
				}
				elseif ($line_trimmed.StartsWith("EndGlobalSection"))
				{
					$insection = $false
					# do not copy this line to output
				}
				elseif ($line_trimmed.StartsWith("Scc"))
				{
					# do not copy this line to output
				}
				else
				{
					if ( !($insection))
					{
						$output_lines.Add( $line )
					}
				}

			}
			$output_lines | Out-File $slnfile.FullName
		}


		# ---------------------------------
		# UNBIND PROJ FILES FROM SOURCE CONTROL
		Write-Verbose Unbinding PROJ files from Source Control
		$projfiles = Get-ChildItem $staging_folder *.*proj -Recurse
		[Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq") | Out-Null
		foreach ($projfile in $projfiles) 
		{
			$doc = [System.Xml.Linq.XDocument]::Load( $projfile.FullName )
			RemoveSCCElementsAttributes($doc.Root);
			$doc.Save( $projfile.FullName )
		}
	}
}

function Assert-TestPath
{
	param
	(
		[parameter(Mandatory=$true)] [string] $Path
	)
	process
	{
	    Write-Verbose "Checking for path $Path"
	    if (Test-Path $Path)
	    {
	        Write-Verbose "SUCCESS: Path exists"		
	    }
	    else
	    {
	        Write-Host "ERROR: Path does not exist"
	        Break    
	    }
	}
}

function AssertFileExists
{
	param
	(
		[parameter(Mandatory=$true)] [string] $Path
	)
	process
	{
	    Write-Verbose "Checking for path $Path"
	    if (Test-Path $Path)
	    {
	        Write-Verbose "SUCCESS: Path exists"		
	    }
	    else
	    {
	        Write-Host "ERROR: Path does not exist"
	        Break    
	    }
	}
}


function DeleteFileIfExists( $filename )
{
    if (test-path $filename )
    {
        Remove-Item $filename 
    }
}


function Export-PowerShellModuleInstaller
{
    param (
        [parameter(Mandatory=$true)] [string] $InputFolder,
        [parameter(Mandatory=$true)] [string] $OutputFolder,
        [parameter(Mandatory=$true)] [string] $WIXBinFolder,
        [parameter(Mandatory=$true)] [string] $ProductNameLong,
        [parameter(Mandatory=$true)] [string] $ProductNameShort,
        [parameter(Mandatory=$true)] [string] $ProductVersion,
        [parameter(Mandatory=$true)] [string] $Manufacturer,
        [parameter(Mandatory=$true)]
		[AllowEmptyString()]
		[string] $ProgramFilesSubFolder,
        [parameter(Mandatory=$true)] [string] $HelpLink,
        [parameter(Mandatory=$true)] [string] $AboutLink,
        [parameter(Mandatory=$true)] [string] $ProductID,
        [parameter(Mandatory=$true)] [string] $UpgradeCode,
        [parameter(Mandatory=$true)] [string] $UpgradeID,	
        [parameter(Mandatory=$true)] 
		[ValidateSet("Default","ProgramFiles","PowerShellUserModule")] 
		[string] $InstallType,
        [parameter(Mandatory=$false)] [string] $KeepTemporaryFolder
		
    )
    PROCESS 
    {
        # ----------------------------------------
        # VERIFY USER INPUT
        Write-Host 
        Write-Verbose "Veryify Paths"
        Assert-TestPath $InputFolder 
        Assert-TestPath $WIXBinFolder 
        Assert-TestPath $OutputFolder 
        Write-Verbose "Finished Veryifying Paths"

        # ----------------------------------------
        # CALCULATE VARIOUS PATHS, FILENAMES, IDS, BASED ON INPUT

        $datestring = Get-Date -format yyyy-MM-dd
        $temp_folder = join-path ([Environment]::GetFolderPath("MyDocuments")) ($ProductNameShort  +"_" + $datestring)
        $cabfilename = $ProductNameShort + ".cab"
        $modules_wxs = join-path $temp_folder ( $ProductNameShort + "_modules.wxs" )
        $product_wxs = join-path $temp_folder ( $ProductNameShort + ".wxs" )
        $varname = "var." + $ProductNameShort
        $heatexe = join-path $WIXBinFolder "heat.exe"
        $candleexe = join-path $WIXBinFolder "candle.exe"
        $lightexe = join-path $WIXBinFolder "light.exe"
        $modules_wixobj = join-path (Get-Location) ( $ProductNameShort  + "_modules.wixobj" )
        $product_wixobj = join-path (Get-Location) ( $ProductNameShort + ".wixobj")
        $directoryid = $ProductNameShort
        $msibasename = $ProductNameShort + "_" + (Get-Date -format yyyy_MM_dd)
        $output_msi_file = join-path $OutputFolder ($msibasename + ".msi")
        $productpdb = join-path (Split-path $output_msi_file) ($msibasename +".wixpdb")
        $licensertf = join-path $InputFolder "license.rtf"
        if (test-path $licensertf)
        {
            $licensecmd = "<WixVariable Id=`"WixUILicenseRtf`" Value=`"License.rtf`"></WixVariable>"
        }
        else
        {
            $licensecmd = ""
        }


        Write-Verbose "Source Folder to Package: $InputFolder"
        Write-Verbose "MSI Will be placed here: $OutputFolder"

        # ----------------------------------------
        # CREATE BEFORE WE BEGIN
        if (test-path $temp_folder)
        {
            # if it already exists, remote it for safety
            Remove-Item $temp_folder -Recurse
        }
        New-Item $temp_folder -ItemType directory | Out-Null

        DeleteFileIfExists($productpdb)
        DeleteFileIfExists($output_msi_file)

        # --------------d:\--------------------------
        # VALIDATE THE BINARIES EXIST
		Write-Verbose "Checking for WIX binaries"
        AssertFileExists $heatexe
        AssertFileExists $candleexe
        AssertFileExists $lightexe
		Write-Verbose "Finished Checking for WIX binaries"


	    # ----------------------------------------
	    # DYNAMICALLY BUILD THE WXS FILE FOR THE MODULES
	    $modules_xml = @"
<?xml version="1.0" encoding="utf-8"?>
<Wix xmlns='http://schemas.microsoft.com/wix/2006/wi'> 
    <Product Id="#productid" 
		Language="1033" 
		Name="#productname" 
		Version="#productversion"
		Manufacturer="#manufacturer"
		UpgradeCode="#upgradecode">
        <Package Description="#productname Installer" 
		InstallPrivileges="elevated" Comments="#productshortname Installer" 
		InstallerVersion="200" Compressed="yes">
	</Package>
        <Upgrade Id="#upgradeid">
            <UpgradeVersion 
		        OnlyDetect="no" 
		        Property="PREVIOUSFOUND" 
		        Minimum="1.0.0" 
		        IncludeMinimum="yes" 
		        Maximum="1.0.0.0"
		        IncludeMaximum="no">
        	</UpgradeVersion>
        </Upgrade>
        <InstallExecuteSequence>
            <RemoveExistingProducts After="InstallInitialize"></RemoveExistingProducts>
        </InstallExecuteSequence>
        <Media Id="1" Cabinet="#cabfilename" EmbedCab="yes"></Media>
        #licensecmd
        <Directory Id="TARGETDIR" Name="SourceDir">
		#installdir
        </Directory>
        <Property Id="ARPHELPLINK" Value="#helplink"></Property>
        <Property Id="ARPURLINFOABOUT" Value="#aboutlink"></Property>
        <Feature Id="#productshortname" Title="#productshortname" Level="1" ConfigurableDirectory="INSTALLDIR">
            <ComponentGroupRef Id="#productshortname">
            </ComponentGroupRef>
        </Feature>
        <UI></UI>
        <UIRef Id="WixUI_InstallDir"></UIRef>
        <Property Id="WIXUI_INSTALLDIR" Value="INSTALLDIR"></Property>
    </Product>
</Wix>
"@

$powershell_user_module_installdir = @"
<Directory Id="PersonalFolder" Name="PersonalFolder">
    <Directory Id="WindowsPowerShell" Name="WindowsPowerShell">
        <Directory Id="INSTALLDIR" Name="Modules">
            <Directory Id="#productshortname" Name="#productshortname">
            </Directory>
        </Directory>
    </Directory>
</Directory>
"@

$program_files_installdir =@"
<Directory Id="ProgramFilesFolder">
        <Directory Id="INSTALLDIR" Name="#progfilessubfolder">
            <Directory Id="#productshortname" Name="#productshortname">
            </Directory>
        </Directory>
</Directory>

"@

		#this has to be done first
		if ($InstallType -eq "PowerShellUserModule")
		{
			$modules_xml = $modules_xml -replace "#installdir", $powershell_user_module_installdir
		}
		elseif( ($InstallType -eq "Default") -or ($InstallType -eq "ProgramFiles"))
		{
			if ( ($ProgramFilesSubFolder -eq $null) -or ($ProgramFilesSubFolder -eq ""))
			{
				Write-Host $ProgramFilesSubFolder is null
				Break
			}
			$modules_xml = $modules_xml -replace "#installdir", $program_files_installdir
		}
		else
		{
			Write-Host Unsupported InstallType
			Break
		}


		$modules_xml = $modules_xml -replace "#productid", $ProductID
	    $modules_xml = $modules_xml -replace "#productname", $ProductNameLong
	    $modules_xml = $modules_xml -replace "#productversion", $ProductVersion
	    $modules_xml = $modules_xml -replace "#manufacturer", $Manufacturer
	    $modules_xml = $modules_xml -replace "#upgradecode", $UpgradeCode
	    $modules_xml = $modules_xml -replace "#productshortname", $ProductNameShort
	    $modules_xml = $modules_xml -replace "#upgradeid", $UpgradeID
	    $modules_xml = $modules_xml -replace "#cabfilename", $cabfilename
	    $modules_xml = $modules_xml -replace "#licensecmd", $licensecmd
	    $modules_xml = $modules_xml -replace "#helplink", $HelpLink
	    $modules_xml = $modules_xml -replace "#aboutlink", $AboutLink
	    $modules_xml = $modules_xml -replace "#licensecmd", $licensecmd
		$modules_xml = $modules_xml -replace "#progfilessubfolder", $ProgramFilesSubFolder

		if ($modules_xml -ccontains "#")
		{
			Write-Host "ERROR: Unreplaced variable in WIX XML"
			Break;
		}

	    $modules_xml = [xml] $modules_xml
	    $modules_xml.Save( $modules_wxs )

        # ----------------------------------------
        # PRODUCE THE WXS FILE
        Write-Verbose "Writing the modules WXS file $modules_wxs"
        $modules_xml.Save( $modules_wxs )
        AssertFileExists $modules_wxs

        Write-Verbose "Using HEAT.EXE to create the product WXS file $product_wxs"
        &$heatexe dir $InputFolder -nologo -sfrag -suid -ag -srd -dir $directoryid  -out $product_wxs -cg $ProductNameShort  -dr $ProductNameShort
        AssertFileExists $product_wxs

        # ----------------------------------------
        # PRODUCE THE WIXOBJ FILES VIA CANDLE
        Write-Verbose "Using CANDLE.EXE to create wixobj files"
        &$candleexe $modules_wxs $product_wxs 
        AssertFileExists $modules_wixobj 
        AssertFileExists $product_wixobj 

        # ----------------------------------------
        # PRODUCE THE MSI VIA LIGHT
        Write-Verbose "Using LIGHT.EXE to create the MSI"
        &$lightexe -ext WixUIExtension -out $output_msi_file $modules_wixobj $product_wixobj -b $InputFolder -sice:ICE91 -sice:ICE69 -sice:ICE38 -sice:ICE57 -sice:ICE64 -sice:ICE204
        AssertFileExists $productpdb
        AssertFileExists $output_msi_file

        # ----------------------------------------
        # CLEANUP 
        if ($KeepTemporaryFolder)
		{
			Write-Verbose "Keeping temporary folder $temp_folder"
			# Do nothing
		}
		else
        {
			Write-Verbose "Removing temporary folder $temp_folder"
			Remove-FolderIfExists -Folder $temp_folder -Description "Temp Folder" -Verbose
        }

        # These have to be manually removed because they don't go into the temp folder by default
		Write-Verbose "Removing wixobj and pdb files produced"
        Remove-FileIfExists -Filename $modules_wixobj -Description "module wixobj" 
        Remove-FileIfExists -Filename  $product_wixobj -Description "product wixobj"
        Remove-FileIfExists -Filename $productpdb -Description "product pdb"

        # ----------------------------------------
        # FINAL MESSAGE
        Write-Host 
        Write-Host "----------------------------------------"
        Write-Host SUCCESS: Installer file created here $output_msi_file 
    }

}


function Export-ZIPFolder
{
    param (
        [parameter(Mandatory=$true)] [string] $InputFolder,
        [parameter(Mandatory=$true)] [string] $OutputFile,
        [parameter(Mandatory=$true)] [bool] $IncludeBaseDir
    )
    PROCESS 
    {
        $asm = [Reflection.Assembly]::LoadWithPartialName( "System.IO.Compression.FileSystem" )
        $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
        [System.IO.Compression.ZipFile]::CreateFromDirectory($InputFolder, $OutputFile, $compressionLevel, $IncludeBaseDir )
    }
}

function Install-PSModule
{
    param (
        [parameter(Mandatory=$true)] [string] $Folder,
        [parameter(Mandatory=$true)] [string] $Name
    )
    PROCESS 
    {
        $script_path = resolve-path $Folder
        $system_ps_folder = $pshome
        $userdocs = join-path $env:USERPROFILE "Documents"
        $userps = join-path $userdocs "WindowsPowerShell"
        $usermodules = join-path $userps "Modules"
        $user_mod_folder = join-path $usermodules $Name

        Create-FolderIfNotExists $userps "User PowerShellFolder"
        Create-FolderIfNotExists $usermodules "User PowerShell Modules Folder"
        Create-FolderIfNotExists $user_mod_folder "Folder for module $Name"
		&robocopy $Folder $user_mod_folder /MIR /NFL /NDL
    }
}

function Uninstall-PSModule
{
    param (
        [parameter(Mandatory=$true)] [string] $Name
    )
    PROCESS 
    {
        $userdocs = join-path $env:USERPROFILE "Documents"
        $userps = join-path $userdocs "WindowsPowerShell"
        $usermodules = join-path $userps "Modules"
        $user_mod_folder = join-path $usermodules $Name

        if (test-path $user_mod_folder)
        {
            Remove-FolderIfExists -Folder $user_mod_folder -Description "Folder for module $Name"
        }
    }
}


function Clean-SolutionFolder
{
    param (
        [parameter(Mandatory=$true)] [string] $Folder
    )
    PROCESS 
    {
        $patterns = @( "debug", "release", "bin" , "obj", "TestResults", "_ReSharper*" )
        foreach ($pattern in $patterns)
        {
            Write-Verbose "PATTERN $pattern"
            $items = @(Get-ChildItem -Path $Folder $pattern -Recurse -Directory)
            Write-Verbose $items.Length
            foreach ($f in $items)
            {
                Write-Host ">" 
                $f.FullName
                Remove-Item $f.FullName -Recurse
            }
        }
    }
}

function Remove-InstalledProduct
{
    param (
        [parameter(Mandatory=$true)] [string] $Name
    )
    PROCESS 
    {
        $filter = "Name = '$Name'"
        $app = Get-WmiObject -Class Win32_Product -Filter $filter
        if ($app -eq $null)
        {
            Write-Verbose "Application is not installed"
        }
        else
        {
            Write-Verbose "Application is installed"
            Write-Verbose "Uninstalling"
            $app.Uninstall()
            Write-Verbose "Finished Uninstalling"    
        }    
    }
}


function Get-EmptyFolders
{
    param (
        [parameter(Mandatory=$true)] [string] $Folder
    )
    PROCESS 
    {
        $folders = Get-ChildItem $Folder -recurse -Directory
        $empty_folders = $folders | Where-Object {$_.GetFiles().Count -eq 0}
        Write-Output $empty_folders
    }
}

