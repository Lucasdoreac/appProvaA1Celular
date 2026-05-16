# Script PowerShell Avançado - WSA 100% Automático
# Autor: Claude Code
# Data: 2026-05-15
# Objetivo: Iniciar WSA e ativar depuração USB automaticamente

Write-Host "🚀 WSA 100% AUTOMÁTICO - Iniciando e configurando..." -ForegroundColor Green

# 1. ENCONTRAR WSA path dinamicamente
Write-Host "1️⃣ Localizando WSA..." -ForegroundColor Cyan
$wsaPath = Get-ChildItem "C:\Program Files\WindowsApps" -Filter "MicrosoftCorporationII.WindowsSubsystemForAndroid_*" -Directory | Select-Object -First 1 -ExpandProperty FullName
if ($wsaPath) {
    Write-Host "   ✅ WSA encontrado: $wsaPath" -ForegroundColor Green
} else {
    Write-Host "   ❌ WSA não encontrado!" -ForegroundColor Red
    exit 1
}

# 2. CRIAR configuração para ativar depuração USB automaticamente
Write-Host "2️⃣ Configurando depuração USB automática..." -ForegroundColor Cyan
$wsaSettingsPath = "$env:LOCALAPPDATA\Packages\MicrosoftCorporationII.WindowsSubsystemForAndroid_8wekyb3d8bbwe\LocalState"

# Criar arquivo de preferências para ativar debug USB
$preferencesJson = @{
    "debugger_blacklist" = @()
    "enable_debug_menu" = $true
    "enable_adb_usb" = $true
    "adb_port" = 58526
} | ConvertTo-Json -Depth 3

$preferencesPath = "$wsaSettingsPath\preferences.json"
$preferencesJson | Out-File -FilePath $preferencesPath -Encoding UTF8
Write-Host "   ✅ Preferências configuradas" -ForegroundColor Green

# 3. INICIAR WSA
Write-Host "3️⃣ Iniciando WSA..." -ForegroundColor Cyan
$wsaClient = "$wsaPath\WsaClient\WsaClient.exe"
Start-Process $wsaClient

# 4. AGUARDAR WSA iniciar completamente
Write-Host "4️⃣ Aguardando WSA iniciar (20 segundos)..." -ForegroundColor Yellow
Start-Sleep -Seconds 20

# 5. VERIFICAR se WSA está rodando
Write-Host "5️⃣ Verificando se WSA está rodando..." -ForegroundColor Cyan
$wsaProcess = Get-Process | Where-Object { $_.ProcessName -like "*WSA*" -or $_.ProcessName -like "*WsaClient*" }
if ($wsaProcess) {
    Write-Host "   ✅ WSA está rodando (PID: $($wsaProcess.Id))" -ForegroundColor Green
} else {
    Write-Host "   ❌ WSA não iniciou corretamente" -ForegroundColor Red
}

# 6. MATAR e reiniciar servidor ADB
Write-Host "6️⃣ Reiniciando servidor ADB..." -ForegroundColor Cyan
$adbPath = "C:\Users\lucas\scoop\shims\adb.exe"
& $adbPath kill-server 2>$null
& $adbPath start-server 2>$null
Start-Sleep -Seconds 2

# 7. TENTAR múltiplas portas WSA
Write-Host "7️⃣ Conectando ADB ao WSA..." -ForegroundColor Cyan
$ports = @(58526, 58525, 5555, 5037)
$connected = $false

foreach ($port in $ports) {
    Write-Host "   Tentando porta $port..." -ForegroundColor Yellow
    $result = & $adbPath connect 127.0.0.1:$port 2>&1
    if ($result -notmatch "refused|cannot connect|failed") {
        Write-Host "   ✅ Conectado na porta $port!" -ForegroundColor Green
        $connected = $true
        break
    }
}

if (-not $connected) {
    Write-Host "   ❌ Não foi possível conectar automaticamente" -ForegroundColor Red
    Write-Host "   💡 Ative 'Depuração USB' manualmente no WSA" -ForegroundColor Yellow
}

# 8. VERIFICAR dispositivos
Write-Host "8️⃣ Verificando dispositivos ADB..." -ForegroundColor Cyan
Start-Sleep -Seconds 2
$devices = & $adbPath devices
Write-Host $devices

# 9. STATUS FINAL
Write-Host "`n🎯 STATUS FINAL:" -ForegroundColor Green
if ($connected) {
    Write-Host "✅ WSA iniciado e ADB conectado!" -ForegroundColor Green
    Write-Host "📱 Dispositivo disponível no Visual Studio dropdown" -ForegroundColor Green
} else {
    Write-Host "⚠️ WSA iniciado mas precisa ativar 'Depuração USB' manualmente" -ForegroundColor Yellow
    Write-Host "📱 GUI do WSA → Configurações → Depuração USB → Ativar" -ForegroundColor Yellow
}

Write-Host "`n🚀 Pronto para rodar appProvaA1Celular no WSA!" -ForegroundColor Green