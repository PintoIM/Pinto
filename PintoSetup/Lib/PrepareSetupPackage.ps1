# Pinto! Setup - PrepareSetupPackage
# Sets up the setup package before building the setup

if ($args.Count -lt 1)
{
    echo 'Not enough arguments provided!'
    exit 1
}

$solutionDir = $args -join " "
$packagePath = "${solutionDir}/PintoSetup/Resources/INSTALL.zip"
[System.Collections.ArrayList]$archiveFiles = @()

foreach ($file in Get-ChildItem "${solutionDir}/Pinto/bin/x86")
{
    $fileName = $file.Name
    if (!$fileName.EndsWith(".exe") -and 
        !$fileName.EndsWith(".dll") -and 
        !$fileName.EndsWith(".pdb") -and
        !$fileName.EndsWith(".config"))
    {
        continue
    }
    echo "Inflating ${fileName}"

    # Apparently you need to cast to remove the stupid output
    [void]$archiveFiles.Add($file.PSPath)
}

Remove-Item $packagePath -ErrorAction Ignore
Compress-Archive -Path $archiveFiles -DestinationPath $packagePath -CompressionLevel Optimal -Force
echo 'Successfully created the setup package!'