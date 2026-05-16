# 🖥️ ALTERNATIVAS 100% CLI - Android Emulation

**Status:** Você tem Android SDK cmdline-tools ✅  
**Problema:** Precisa de Java para usar sdkmanager/avdmanager ❌

---

## 🚀 OPÇÃO 1: Emulador Android CLI (NATIVO)

### Pré-requisito: Java
```bash
# Instalar Java (JDK) via scoop
scoop install openjdk17

# Ou baixar diretamente:
https://adoptium.net/temurin/releases/?version=17
```

### Criar Emulador via CLI:
```bash
# 1. Set JAVA_HOME
export JAVA_HOME="C:\Program Files\Eclipse Adoptium\jdk-17.0.11.9-hotspot"

# 2. Baixar system image
"C:/Users/lucas/AppData/Local/Android/Sdk/cmdline-tools/latest/bin/sdkmanager.bat" "system-images;android-33;google_apis;x86_64"

# 3. Criar AVD
"C:/Users/lucas/AppData/Local/Android/Sdk/cmdline-tools/latest/bin/avdmanager.bat" create avd -n pixel6 -k "system-images;android-33;google_apis;x86_64" -d

# 4. Iniciar emulador
"C:/Users/lucas/AppData/Local/Android/Sdk/emulator/emulator.exe" -avd pixel6
```

---

## 🚀 OPÇÃO 2: Docker Android (⭐ RECOMENDADO CLI)

### Pré-requisito: Docker Desktop
```bash
# Verificar se Docker está rodando
docker ps
```

### Imagens Android Docker:
```bash
# Opção A: Dockroid (Android container)
docker run -d --privileged dockroid/android:13

# Opção B: Anbox (Linux-based)
docker run -d --privileged anbox/android

# Opção C: Web-based Android
docker run -d -p 6080:6080 budtmo/docker-android:studio
```

### Conectar ADB ao container:
```bash
# Encontrar IP do container
docker ps
docker inspect <container_id> | grep IPAddress

# Conectar ADB
adb connect <container_ip>:5555
```

---

## 🚀 OPÇÃO 3: WSA CLI (Windows Subsystem for Android)

### Via PowerShell:
```powershell
# Instalar WSA via CLI
winget install 9P3395VX91R8  # WSA com Amazon Appstore

# Ou versão sem Amazon
winget install 9P8KPZW3SNZ8

# Iniciar WSA
C:\Users\lucas\AppData\Local\Microsoft\WindowsApps\MicrosoftCorporationII.WindowsSubsystemForAndroid_8wekyb3d8bbwe\WsaClient.exe
```

### Conectar ADB ao WSA:
```bash
# WSA usa porta 58526 por padrão
adb connect 127.0.0.1:58526

# Verificar conexão
adb devices
```

---

## 🚀 OPÇÃO 4: Genymotion CLI (Profissional)

### Instalar Genymotion:
```bash
# Download CLI
https://www.genymotion.com/download-linux

# Ou versão completa com GUI + CLI
https://www.genymotion.com/download
```

### Criar dispositivo via CLI:
```bash
# Genymotion CLI tool
gmtool create device --name "pixel_6" --android "13.0"

# Iniciar via CLI
gmtool start --name "pixel_6"
```

---

## 🚀 OPÇÃO 5: AVD Manager (Manual mas CLI-friendly)

### Criar AVD via GUI + Iniciar via CLI:
```bash
# 1. Criar AVD usando AVD Manager GUI (android avd)
# 2. Iniciar via CLI:

"C:/Users/lucas/AppData/Local/Android/Sdk/emulator/emulator.exe" -avd <nome_avd>

# 3. Listar AVDs disponíveis
"C:/Users/lucas/AppData/Local/Android/Sdk/emulator/emulator.exe" -list-avds

# 4. Iniciar em modo headless (sem GUI)
"C:/Users/lucas/AppData/Local/Android/Sdk/emulator/emulator.exe" -avd <nome_avd> -no-window -no-audio
```

---

## 🎯 RECOMENDAÇÃO CLI

### **Opção 1: WSA via winget** (⭐ MAIS FÁCIL)
```bash
# 1 comando instala, 1 comando conecta ADB
winget install 9P3395VX91R8
adb connect 127.0.0.1:58526
```

### **Opção 2: NoxPlayer + ADB** (⭐ MAIS RÁPIDO)
```bash
# GUI leve + CLI do ADB
# Instalar Nox → conectar ADB → pronto
adb connect 127.0.0.1:62001
```

### **Opção 3: Docker Android** (⭐ MAIS "DEV")
```bash
# 100% CLI, mas precisa Docker
docker run -d --privileged dockroid/android:13
```

---

## 📋 COMPARATIVO CLI

| Opção | Instalação | Uso CLI | Performance | Facilidade |
|:---|:---|:---|:---|:---|
| **WSA + winget** | 1 comando | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **NoxPlayer + ADB** | GUI + CLI | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Docker Android** | 1 comando | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐ |
| **AVD CLI** | Complexo | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | ⭐⭐ |

---

## 🚀 MINI-GUIA: WSA CLI (Recomendado)

```bash
# 1. Instalar WSA (1 comando)
winget install 9P3395VX91R8

# 2. Abrir WSA uma vez (GUI abre, depois pode fechar)

# 3. Conectar ADB (1 comando)
adb connect 127.0.0.1:58526

# 4. Verificar
adb devices

# 5. No VS: Dropdown mostra "WSA"
```

---

**Qual opção CLI prefere?**

1. **WSA** (1 comando instala, melhor integração Windows)
2. **Docker** (100% CLI, mas precisa Docker instalado)
3. **AVD nativo** (requer Java, mais complexo)
4. **Nox + ADB** (GUI leve + CLI ADB)