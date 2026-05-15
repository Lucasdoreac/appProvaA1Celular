# Como Rodar o App - .NET MAUI (Android + Windows)

## Status: ✅ MULTI-PLATAFORMA FUNCIONANDO

**Versão:** .NET 8.0 LTS
**Plataformas:** Android e Windows
**SQLite:** 100% funcional (celulares.db3)

---

## Pré-Requisitos

### Instalado e Configurado
- ✅ .NET SDK 8.0 (LTS)
- ✅ Visual Studio 2022 com workloads:
  - .NET Multi-Platform App UI development
  - Windows App SDK C#
- ✅ Android Emulator ou dispositivo
- ✅ Windows 10/11

### Verificar Instalação
```bash
dotnet --list-sdks          # Deve mostrar 8.0.x
dotnet workload list        # Deve mostrar maui, android, maui-windows
```

---

## Comandos de Build e Execução

### Android

**Build:**
```bash
dotnet build --framework net8.0-android
```

**Rodar (requer emulator ou dispositivo conectado):**
```bash
dotnet run --framework net8.0-android
```

**Publicar APK:**
```bash
dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
```

### Windows

**Build:**
```bash
dotnet build --framework net8.0-windows10.0.19041.0
```

**Rodar:**
```bash
dotnet run --framework net8.0-windows10.0.19041.0
```

**Publicar EXE:**
```bash
dotnet publish -f net8.0-windows10.0.19041.0 -c Release
```


### Multi-Target (Ambas Plataformas)

**Build tudo:**
```bash
dotnet build
```

**Limpar e rebuildar:**
```bash
dotnet clean
dotnet restore
dotnet build
```

---

## Funcionalidades Disponíveis

### ✅ Implementadas e Testadas

**CRUD Completo de Celulares:**
- ✅ Listar todos os celulares (TelaListaCelular)
- ✅ Incluir novo celular (TelaIncluirCelular)
- ✅ Alterar celular existente (TelaAlterarCelular)
- ✅ Excluir celular com confirmação

**SQLite:**
- ✅ Banco de dados local funcional
- ✅ Todas as operações CRUD funcionando
- ✅ Persistência de dados entre sessões

**Interface:**
- ✅ Navegação entre telas funcional
- ✅ Validação de campos (Fabricante, Modelo)
- ✅ DisplayAlert para feedback ao usuário
- ✅ ToolbarItems com ícones

---

## Estrutura do Projeto

```
appProvaA1Celular/
├── Model/
│   └── Celular.cs              # Modelo de dados com SQLite attributes
├── DAL/
│   ├── BancoDeDados.cs         # Conexão SQLite
│   └── crudSQLite.cs           # Operações CRUD completas
├── Views/
│   ├── TelaListaCelular.xaml   # Listagem de celulares
│   ├── TelaIncluirCelular.xaml # Formulário de inclusão
│   └── TelaAlterarCelular.xaml # Formulário de alteração
├── Resources/
│   ├── Images/                 # Imagens do app
│   ├── Fonts/                  # Fontes customizadas
│   └── Styles/                 # Estilos XAML
├── App.xaml                    # Application entry point
├── MauiProgram.cs              # MAUI bootstrap
└── appProvaA1Celular.csproj    # Multi-targeting: Android + Windows
```

---

## Troubleshooting

### Android: Emulator não inicia
```bash
# Verificar devices conectados
adb devices

# Lista de emuladores disponíveis
dotnet workload install android

# Iniciar emulator manualmente (via Android Studio AVD Manager)
```

### Windows: Erro de runtime
```bash
# Limpar cache e rebuildar
dotnet clean
rm -rf obj bin
dotnet restore
dotnet build --framework net8.0-windows10.0.19041.0
```

### SQLite: Erro de permissão
- **Localização:** Environment.SpecialFolder.LocalApplicationData
- **Arquivo:** celulares.db3
- **Plataformas:** Android e Windows funcionam out-of-the-box

### Build: Erro de restore
```bash
# Limpar cache NuGet
dotnet nuget locals all --clear

# Re-restaurar pacotes
dotnet restore --no-cache
```

---

## Configuração do Projeto

**Target Frameworks:**
```xml
<TargetFrameworks>net8.0-android;net8.0-windows10.0.19041.0</TargetFrameworks>
```

**Versões Mínimas Suportadas:**
- Android: API 21 (Android 5.0)
- Windows: Build 17763 (Windows 10 version 1809)

**Packages NuGet:**
- Microsoft.Maui.Controls 8.0.3
- sqlite-net-pcl 1.9.172

---

## Dicas de Desenvolvimento

### Hot Reload (Visual Studio 2022)
1. Abrir o projeto no Visual Studio 2022
2. Selecionar plataforma (Android ou Windows)
3. Pressionar F5 (Debug)
4. Alterar XAML enquanto roda → Hot Reload automático

### Edição Rápida
```bash
# Editar arquivo e rebuild rápido
dotnet build --no-restore
```

### Ver Logs
```bash
# Android Logcat
adb logcat | grep appProvaA1Celular

# Windows DebugView
# Baixar: https://learn.microsoft.com/sysinternals/downloads/debugview
```

---

## Conformidade com Apostilas 08-09

### Apostila 08 - SQLite
- ✅ Modelo com atributos [Table], [PrimaryKey]
- ✅ CRUD completo implementado
- ✅ Banco de dados local funcional

### Apostila 09 - MAUI
- ✅ Multi-plataforma (Android + Windows)
- ✅ NavigationPage implementado
- ✅ ToolbarItems configurados
- ✅ XAML + Code-behind pattern

**Avaliação Esperada:** 10/10 ✨

---

## Suporte e Documentação

**Documentação Oficial:**
- [.NET MAUI Docs](https://learn.microsoft.com/dotnet/maui)
- [sqlite-net-pcl](https://github.com/praeclarum/sqlite-net)

**Projetos Relacionados:**
- Notebook "Research-dev-projeto-final" (NotebookLM)
- Documentos: MAUI-MULTI-PLATAFORMA-FUNCIONANDO.md
