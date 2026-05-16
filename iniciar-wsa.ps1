# Script PowerShell para iniciar WSA automaticamente
# Autor: Claude Code
# Data: 2026-05-15

Write-Host "🚀 Iniciando Windows Subsystem for Android automaticamente..." -ForegroundColor Green

# 1. Iniciar WSA
Write-Host "1️⃣ Iniciando WSA..." -ForegroundColor Cyan
Start-Process "C:\Program Files\WindowsApps\MicrosoftCorporationII.WindowsSubsystemForAndroid_2407.40000.4.0_x64__8wekyb3d8bbwe\WsaClient\WsaClient.exe"

# 2. Aguardar WSA iniciar
Write-Host "2️⃣ Aguardando WSA iniciar (15 segundos)..." -ForegroundColor Cyan
Start-Sleep -Seconds 15

# 3. Tentar conectar ADB
Write-Host "3️⃣ Conectando ADB ao WSA..." -ForegroundColor Cyan
$adbPath = "C:\Users\lucas\scoop\shims\adb.exe"
& $adbPath connect 127.0.0.1:58526

# 4. Verificar conexão
Write-Host "4️⃣ Verificando dispositivos..." -ForegroundColor Cyan
Start-Sleep -Seconds 3
& $adbPath devices

Write-Host "✅ WSA iniciado e ADB conectado!" -ForegroundColor Green
Write-Host "📱 Dispositivo disponível no Visual Studio dropdown" -ForegroundColor Yellow