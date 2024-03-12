# Pinto! Setup - CleanSetupPackage
# Cleans up the setup package after building the setup

if ($args.Count -lt 1)
{
    echo 'Not enough arguments provided!'
    exit 1
}

$solutionDir = $args -join " "
$packagePath = "${solutionDir}/PintoSetup/Resources/INSTALL.zip"

Remove-Item $packagePath -ErrorAction Ignore
Set-Content -Path $packagePath -Value @()
echo 'Successfully cleaned up the setup package!'