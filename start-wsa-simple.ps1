# Script PowerShell Simples - WSA Automático
# Autor: Claude Code
# Data: 2026-05-15

Write-Host "🚀 WSA AUTOMÁTICO - Iniciando e configurando..." -ForegroundColor Green

# 1. INICIAR WSA (path direto)
Write-Host "1️⃣ Iniciando WSA..." -ForegroundColor Cyan
Start-Process -FilePath "C:\Program Files\WindowsApps\MicrosoftCorporationII.WindowsSubsystemForAndroid_2407.40000.4.0_x64__8wekyb3d8bbwe\WsaClient\WsaClient.exe"

# 2. AGUARDAR WSA iniciar
Write-Host "2️⃣ Aguardando WSA iniciar (15 segundos)..." -ForegroundColor Yellow
Start-Sleep -Seconds 15

# 3. REINICIAR ADB
Write-Host "3️⃣ Reiniciando servidor ADB..." -ForegroundColor Cyan
$adbPath = "C:\Users\lucas\scoop\shims\adb.exe"
& $adbPath kill-server 2>$null
& $adbPath start-server 2>$null
Start-Sleep -Seconds 3

# 4. TENTAR conectar ADB
Write-Host "4️⃣ Tentando conectar ADB..." -ForegroundColor Cyan
$ports = @(58526, 58525, 5555)
$connected = $false

foreach ($port in $ports) {
    $result = & $adbPath connect 127.0.0.1:$port 2>&1
    if ($result -notmatch "refused|cannot connect|failed") {
        Write-Host "   ✅ Conectado na porta $port!" -ForegroundColor Green
        $connected = $true
        break
    }
}

# 5. VERIFICAR dispositivos
Write-Host "5️⃣ Verificando dispositivos..." -ForegroundColor Cyan
Start-Sleep -Seconds 2
$devices = & $adbPath devices
Write-Host $devices

# 6. INSTRUÇÕES se não conectou
if (-not $connected) {
    Write-Host "`n⚠️ NÃO CONECTADO AUTOMATICAMENTE" -ForegroundColor Yellow
    Write-Host "💡 Ative 'Depuração USB' no WSA:" -ForegroundColor Yellow
    Write-Host "   1. GUI do WSA deve estar aberta" -ForegroundColor Yellow
    Write-Host "   2. Configurações (⚙️) → Depuração USB" -ForegroundColor Yellow
    Write-Host "   3. Ativar 'Depuração USB'" -ForegroundColor Yellow
    Write-Host "   4. Execute: adb connect 127.0.0.1:58526" -ForegroundColor Yellow
} else {
    Write-Host "`n✅ SUCESSO! WSA disponível no VS dropdown" -ForegroundColor Green
}