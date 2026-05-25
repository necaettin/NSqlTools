# SignTool ile NSqlTools.exe'yi imzalama scripti
# Run in normal PowerShell (Administrator gerekmez)

Write-Host "Finding SignTool.exe..." -ForegroundColor Cyan
$signTool = Get-ChildItem "C:\Program Files (x86)\Windows Kits" -Recurse -Filter "signtool.exe" -ErrorAction SilentlyContinue | Select-Object -First 1 -ExpandProperty FullName

if (-not $signTool) {
    Write-Host "ERROR: SignTool.exe not found!" -ForegroundColor Red
    Write-Host "Please install Windows SDK from: https://developer.microsoft.com/en-us/windows/downloads/windows-sdk/" -ForegroundColor Yellow
    exit 1
}

Write-Host "SignTool found: $signTool" -ForegroundColor Green

# PFX ve EXE path'leri
$pfxPath = Join-Path $PSScriptRoot "NSqlTools_CodeSign.pfx"
$exePathRelease = Join-Path $PSScriptRoot "NSqlTools.UI\bin\Release\NSqlTools.exe"
$exePathDebug = Join-Path $PSScriptRoot "NSqlTools.UI\bin\Debug\NSqlTools.exe"

# Hangi EXE var kontrol et
$exePath = $null
if (Test-Path $exePathRelease) {
    $exePath = $exePathRelease
    Write-Host "Release build found" -ForegroundColor Green
} elseif (Test-Path $exePathDebug) {
    $exePath = $exePathDebug
    Write-Host "Debug build found" -ForegroundColor Green
} else {
    Write-Host "ERROR: NSqlTools.exe not found in Release or Debug folder!" -ForegroundColor Red
    Write-Host "Please build the project first." -ForegroundColor Yellow
    exit 1
}

if (-not (Test-Path $pfxPath)) {
    Write-Host "ERROR: NSqlTools_CodeSign.pfx not found!" -ForegroundColor Red
    Write-Host "Please run CreateSelfSignedCert.ps1 first." -ForegroundColor Yellow
    exit 1
}

Write-Host "Signing $exePath..." -ForegroundColor Cyan
& $signTool sign /f $pfxPath /p "NSqlTools2024!" /fd SHA256 /t http://timestamp.digicert.com /v $exePath

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nSigning successful!" -ForegroundColor Green
    
    # Verify signature
    Write-Host "`nVerifying signature..." -ForegroundColor Cyan
    & $signTool verify /pa /v $exePath
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "`nVerification successful!" -ForegroundColor Green
        Write-Host "`nYou can now distribute:" -ForegroundColor Yellow
        Write-Host "1. $exePath" -ForegroundColor White
        Write-Host "2. $pfxPath (password: NSqlTools2024!)" -ForegroundColor White
    }
} else {
    Write-Host "`nSigning failed! Error code: $LASTEXITCODE" -ForegroundColor Red
}
