# Run as Administrator
$certName = "NSqlTools Developer"
$certPassword = "NSqlTools2024!"

Write-Host "Creating self-signed code signing certificate..." -ForegroundColor Green
$cert = New-SelfSignedCertificate -Type CodeSigningCert -Subject "CN=$certName" -CertStoreLocation Cert:\CurrentUser\My -NotAfter (Get-Date).AddYears(5)

Write-Host "Exporting certificate to PFX..." -ForegroundColor Green
$pwd = ConvertTo-SecureString -String $certPassword -Force -AsPlainText
$pfxPath = Join-Path $PSScriptRoot "NSqlTools_CodeSign.pfx"
Export-PfxCertificate -Cert $cert -FilePath $pfxPath -Password $pwd

Write-Host "Certificate created successfully!" -ForegroundColor Green
Write-Host "Certificate file: $pfxPath" -ForegroundColor Yellow
Write-Host "Password: $certPassword" -ForegroundColor Yellow
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "1. Sign your EXE using SignTool.exe" -ForegroundColor White
Write-Host "2. Distribute the PFX to users and have them install it to Trusted Root CA store" -ForegroundColor White
