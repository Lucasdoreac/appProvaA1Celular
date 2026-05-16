# 🎮 GUIA PASSO A PASSO - Instalação Android Emulator

**Objetivo:** Instalar Android Emulator oficial para desenvolvimento .NET MAUI  
**Tempo estimado:** 15-30 minutos  
**Espaço necessário:** 4-8 GB

---

## 🚀 PASSO A PASSO

### 1. Visual Studio Installer (JÁ ABERTO) ✅
O Visual Studio Installer foi aberto automaticamente!

### 2. Modificar Instalação
1. **No Visual Studio Installer:**
   - Clique em **"Modificar"** na sua instalação do Visual Studio
   - Aguarde carregar as opções

### 3. Componentes Individuais
1. **Aba "Componentes Individuais"**
2. **Pesquisar por:** "Android Emulator"
3. **Marcar as seguintes opções:**

#### ✅ OBRIGATÓRIOS:
- [ ] **Android Emulator** (API Level 33 ou 34)
- [ ] **Android SDK Platform 33** ou **Android SDK Platform 34**
- [ ] **Android Build Tools**

#### ✅ RECOMENDADOS:
- [ ] **Intel HAXM** (se processador Intel)
- [ ] **Google USB Driver** (para dispositivos físicos)

### 4. SDK Platforms
1. **Aba "SDKs"**
2. **Android SDK:**
   - Garantir que **Android 13.0 (API 33)** ou **Android 14.0 (API 34)** está marcado
   - Garantir que **Android 9.0 (API 28)** está marcado (mínimo para MAUI)

### 5. Confirmar Instalação
1. **Clique em "Modificar"**
2. **Aguarde download e instalação** (pode levar 15-30 min)
3. **Clique em "Fechar" quando terminar

---

## 📱 APÓS INSTALAÇÃO

### 1. Criar Emulador Virtual
1. **Abrir Visual Studio**
2. **Tools → Android → Android Device Manager**
3. **Clique em "+" para criar novo dispositivo**

### 2. Configurar Dispositivo
1. **Escolher hardware:**
   - **Pixel 6** ou **Pixel 7** (recomendado)
   - Ou qualquer dispositivo com **Play Store**

2. **Escolher system image:**
   - **API 33 (Android 13)** ou **API 34 (Android 14)**
   - **Recommended** ou **Release** (não usar "Debug")

3. **Configurar:**
   - **RAM:** 2048 MB (mínimo) ou 4096 MB (recomendado)
   - **VM Heap:** 512 MB
   - **Internal Storage:** 2048 MB
   - **SD Card:** Opcional

4. **Finalizar:** Clique em "Create" e aguarde criação

---

## 🎮 PRIMEIRO USO

### No Visual Studio:
1. **Abrir solução:** `appProvaA1Celular.sln`
2. **Dropdown de dispositivos:** Selecionar o emulador Android criado
3. **Pressionar F5** (Debug)

### Primeiro start pode demorar:
- **Boot inicial:** 2-5 minutos (seja paciente!)
- **Próximos boots:** 30-60 segundos

---

## ⚠️ SOLUÇÃO DE PROBLEMAS

### HAXM não funciona (Processador AMD/Intel)
**Solução:**
1. Desabilitar HAXM
2. Usar **hypervisor** do Windows (Hyper-V)

### Emulador lento
**Solução:**
1. Aumentar RAM do emulador
2. Usar **x86_64** ao invés de **ARM64** (se seu PC for x86)
3. Ativar **VT-x/AMD-V** na BIOS

### Erro de BIOS
**Solução:**
1. Entrar na BIOS (F2, Delete, ou Esc)
2. Ativar **Intel VT-x** ou **AMD-V**
3. Ativar **Hyper-V** (Windows)

---

## 📊 PRÓXIMO PASSO APÓS INSTALAÇÃO

1. **Criar emulador virtual**
2. **Testar com Hello World**
3. **Rodar appProvaA1Celular**
4. **Testar todas as funcionalidades**

---

**Status:** Aguardando você completar a instalação no Visual Studio Installer 🎯

**Dica:** O download pode demorar, mas o emulador oficial é o melhor para desenvolvimento .NET MAUI!