# CÓDIGO COMPLETO appProvaA1Celular - ANÁLISE PROFESSOR

## ARQUIVO 1: App.xaml
```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="appProvaA1Celular.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

## ARQUIVO 2: App.xaml.cs
```csharp
using Microsoft.Maui.Controls;
using appProvaA1Celular.Views;

namespace appProvaA1Celular
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // CORREÇÃO CRÍTICA: Resolver problema de contraste em ToolbarItems
            // Conforme recomendação NLM + Microsoft Learn para acessibilidade
            var navigationPage = new NavigationPage(new TelaListaCelular())
            {
                // Cor de fundo da barra de navegação (tema roxo do app)
                BarBackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#512BD4"),

                // Cor do texto e ícones da barra (branco para contraste máximo)
                BarTextColor = Microsoft.Maui.Graphics.Colors.White
            };

            MainPage = navigationPage;
        }
    }
}
```

## ARQUIVO 3: appProvaA1Celular.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFrameworks>net8.0-android;net8.0-windows10.0.19041.0</TargetFrameworks>
        <TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('Linux'))">net8.0-android</TargetFrameworks>
        <!-- Uncomment to also build the tizen app. You will need to install tizen by following this: https://github.com/Samsung/Tizen.NET -->
        <!-- <TargetFrameworks>net8.0-tizen</TargetFrameworks> -->
        <Note>VS 17.8 projects should use net9.0 target frameworks, but net8.0 is still supported via the SDK versions in global.json if you don't have the .NET 9 SDK.</Note>
        <SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)') == 'ios')">15.4</SupportedOSPlatformVersion>
        <SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)') == 'maccatalyst')">15.4</SupportedOSPlatformVersion>
        <SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)') == 'android')">34.0</SupportedOSPlatformVersion>
        <SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)') == 'windows')">10.0.22621.0</SupportedOSPlatformVersion>
        <TargetPlatformMinVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)') == 'windows')">10.0.17763.0</TargetPlatformMinVersion>
        <SupportedOSPlatformVersion Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)') == 'tizen')">9.0</SupportedOSPlatformVersion>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.Maui.Controls" Version="8.0.82" />
        <PackageReference Include="Microsoft.Maui.Controls.Compatibility" Version="8.0.82" />
        <PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="9.0.0" />
        <PackageReference Include="sqlite-net-pcl" Version="1.9.172" />
        <PackageReference Include="SQLitePCLRaw.bundle_green" Version="1.1.12" />
    </ItemGroup>

</Project>
```

## ARQUIVO 4: DAL/BancoDeDados.cs
```csharp
using System;
using System.IO;
using SQLite;
using appProvaA1Celular.Model;

namespace appProvaA1Celular.DAL
{
    public class BancoDeDados
    {
        private static crudSQLite _database;
        private static readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "celulares.db3");

        public static crudSQLite Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new crudSQLite(_dbPath);
                }
                return _database;
            }
        }
    }
}
```

## ARQUIVO 5: DAL/crudSQLite.cs
```csharp
using SQLite;
using appProvaA1Celular.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace appProvaA1Celular.DAL
{
    public class crudSQLite
    {
        private readonly SQLiteAsyncConnection _connection;

        public crudSQLite(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        private bool _isInitialized = false;

        // REGRA DE OURO DO PROFESSOR: .Wait() assegura que o banco esteja pronto antes do uso
        // Conforme Apostilas 08-09, o construtor DAL deve usar .Wait() para garantir inicialização
        private void InitAsync()
        {
            if (_isInitialized) return;
            _connection.CreateTableAsync<Celular>().Wait();
            _isInitialized = true;
        }

        // Create - Inserir novo celular
        public async Task<bool> IncluirCelularAsync(Celular celular)
        {
            await InitAsync();
            await _connection.InsertAsync(celular);
            return true;
        }

        // Read - Obter todos os celulares
        public async Task<ObservableCollection<Celular>> GetCelularesAsync()
        {
            await InitAsync();
            var celulares = await _connection.Table<Celular>().ToListAsync();
            return new ObservableCollection<Celular>(celulares);
        }

        // Update - Alterar celular existente
        public async Task<bool> AlterarCelularAsync(Celular celular)
        {
            await InitAsync();
            await _connection.UpdateAsync(celular);
            return true;
        }

        // Delete - Excluir celular
        public async Task<bool> ExcluirCelularAsync(Celular celular)
        {
            await InitAsync();
            await _connection.DeleteAsync(celular);
            return true;
        }

        // GetByID - Obter celular por ID
        public async Task<Celular> GetCelularByIdAsync(int id)
        {
            await InitAsync();
            return await _connection.Table<Celular>().Where(c => c.celID == id).FirstOrDefaultAsync();
        }
    }
}
```

## ARQUIVO 6: MauiProgram.cs
```csharp
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Extensions.Logging;

namespace appProvaA1Celular
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
```

## ARQUIVO 7: Model/Celular.cs
```csharp
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace appProvaA1Celular.Model
{
    [Table("Celular")]
    public class Celular : INotifyPropertyChanged
    {
        private int _celID;
        private string _celFabricante;
        private string _celModelo;

        [PrimaryKey, AutoIncrement, Unique, NotNull]
        public int celID
        {
            get => _celID;
            set
            {
                _celID = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(1000)]
        public string celFabricante
        {
            get => _celFabricante;
            set
            {
                _celFabricante = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(1000)]
        public string celModelo
        {
            get => _celModelo;
            set
            {
                _celModelo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```

## ARQUIVO 8: Views/TelaListaCelular.xaml
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             x:Class="appProvaA1Celular.Views.TelaListaCelular"
             Title="Meus Celulares">

    <!-- ToolbarItem para adicionar novo celular -->
    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Adicionar"
                     IconImageSource="incluircelular.png"
                     Clicked="OnAdicionarClicked"
                     SemanticProperties.Hint="Cadastrar um novo celular no sistema"
                     Order="Primary" />
    </ContentPage.ToolbarItems>

    <Grid RowDefinitions="Auto,Auto,*,Auto" Padding="10" RowSpacing="10">

        <!-- Header -->
        <Label Grid.Row="0"
               Text="Gerenciamento de Celulares"
               FontSize="24"
               FontAttributes="Bold"
               HorizontalTextAlignment="Center"
               Margin="0,0,0,10" />

        <!-- SearchBar para filtrar celulares -->
        <SearchBar Grid.Row="1"
                   x:Name="txtBusca"
                   Placeholder="Buscar celular por fabricante ou modelo..."
                   SearchButtonPressed="OnSearchButtonPressed"
                   Margin="0,0,0,10" />

        <!-- Lista de Celulares -->
        <ListView Grid.Row="2"
                  x:Name="lstCelulares"
                  IsPullToRefreshEnabled="True"
                  Refreshing="refCarregando"
                  RowHeight="80"
                  ItemSelected="OnItemSelected">

            <!-- ListView.Header conforme Apostila 09 -->
            <ListView.Header>
                <Grid ColumnDefinitions="100,*,150" Padding="10" BackgroundColor="{AppThemeBinding Light=#F0F0F0, Dark=#2A2A2A}">
                    <Label Grid.Column="0" Text="ID" FontAttributes="Bold" />
                    <Label Grid.Column="1" Text="Fabricante / Modelo" FontAttributes="Bold" />
                    <Label Grid.Column="2" Text="Ações" FontAttributes="Bold" HorizontalTextAlignment="Center" />
                </Grid>
            </ListView.Header>

            <ListView.ItemTemplate>
                <DataTemplate>
                    <ViewCell>
                        <ViewCell.ContextActions>
                            <MenuItem Clicked="OnExcluirContextAction" Text="Excluir Celular" CommandParameter="{Binding}" />
                        </ViewCell.ContextActions>
                        <Border Margin="5" Padding="10" StrokeShape="RoundRectangle 8,0" BackgroundColor="{AppThemeBinding Light=White, Dark=#1F1F1F}">
                            <Grid ColumnDefinitions="Auto,*,Auto" RowDefinitions="Auto,Auto">

                                <!-- Ícone -->
                                <Label Grid.Column="0"
                                       Grid.RowSpan="2"
                                       Text="📱"
                                       FontSize="32"
                                       VerticalOptions="Center" />

                                <!-- Informações -->
                                <Label Grid.Column="1"
                                       Grid.Row="0"
                                       Text="{Binding celFabricante}"
                                       FontSize="18"
                                       FontAttributes="Bold" />

                                <Label Grid.Column="1"
                                       Grid.Row="1"
                                       Text="{Binding celModelo}"
                                       FontSize="14"
                                       Opacity="0.7" />

                                <!-- Botão Excluir -->
                                <Button Grid.Column="2"
                                        Grid.RowSpan="2"
                                        Text="🗑️"
                                        FontSize="20"
                                        Clicked="OnExcluirClicked"
                                        CommandParameter="{Binding}"
                                        BackgroundColor="{AppThemeBinding Light=Red, Dark=DarkRed}"
                                        TextColor="White"
                                        WidthRequest="50"
                                        HeightRequest="50"
                                        CornerRadius="25"
                                        VerticalOptions="Center"
                                        SemanticProperties.Hint="Excluir este celular da lista" />

                            </Grid>
                        </Border>
                    </ViewCell>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>

        <!-- Botão Adicionar -->
        <Button Grid.Row="3"
                Text="➕ Adicionar Celular"
                FontSize="18"
                FontAttributes="Bold"
                Clicked="OnAdicionarClicked"
                HeightRequest="55"
                CornerRadius="10"
                BackgroundColor="{AppThemeBinding Light=#512BD4, Dark=#7B61FF}"
                TextColor="White" />

    </Grid>

</ContentPage>
```

## ARQUIVO 9: Views/TelaListaCelular.xaml.cs
```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using appProvaA1Celular.Model;
using appProvaA1Celular.DAL;

namespace appProvaA1Celular.Views
{
    public partial class TelaListaCelular : ContentPage
    {
        // REGRA DO PROFESSOR: ObservableCollection é obrigatória para ListView
        // Conforme Apostila 09, a ObservableCollection atualiza a UI automaticamente
        private System.Collections.ObjectModel.ObservableCollection<Celular> _todosCelulares =
            new System.Collections.ObjectModel.ObservableCollection<Celular>();

        public TelaListaCelular()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await refCarregando();
        }

        // Método para recarregar a lista de celulares
        private async Task refCarregando()
        {
            try
            {
                var db = BancoDeDados.Database;
                var celulares = await db.GetCelularesAsync();

                // REGRA DO PROFESSOR: Limpar e preencher ObservableCollection
                // Conforme Apostila 09, a lista deve ser limpa e repovada a cada OnAppearing
                _todosCelulares.Clear();
                foreach (var celular in celulares)
                {
                    _todosCelulares.Add(celular);
                }

                lstCelulares.ItemsSource = _todosCelulares;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro !!!!", $"Erro ao carregar: {ex.Message}", "OK");
            }
        }

        // Evento de Refreshing para Pull to Refresh (ListView)
        private async void refCarregando(object sender, EventArgs e)
        {
            await refCarregando();
            lstCelulares.IsRefreshing = false;
        }

        // Evento de seleção de um item na ListView
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                // Celular que foi selecionado na ListView
                if (e.SelectedItem is Celular celular1)
                {
                    await Navigation.PushAsync(new TelaAlterarCelular { BindingContext = celular1 });
                    // Limpar seleção após navegação
                    ((ListView)sender).SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro Desconhecido na Seleção de Celular !!!!", ex.Message, "OK");
            }
        }

        // Evento do botão Adicionar
        private async void OnAdicionarClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new TelaIncluirCelular());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro no Cadastro de Celular !!!!", ex.Message, "OK");
            }
        }

        // Evento da SearchBar para filtrar celulares
        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtBusca.Text.ToLower();

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    // Se busca vazia, mostrar todos os celulares
                    lstCelulares.ItemsSource = _todosCelulares;
                }
                else
                {
                    // Filtrar por fabricante ou modelo
                    var filtrados = _todosCelulares
                        .Where(c => c.celFabricante.ToLower().Contains(searchTerm) ||
                                    c.celModelo.ToLower().Contains(searchTerm))
                        .ToList();

                    lstCelulares.ItemsSource = filtrados;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro !!!!", $"Erro ao filtrar: {ex.Message}", "OK");
            }
        }

        // Evento do botão Excluir
        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.CommandParameter is Celular celular)
                {
                    bool confirm = await DisplayAlert(
                        "Confirmar !!!!",
                        $"Deseja excluir {celular.celFabricante} {celular.celModelo}?",
                        "Sim",
                        "Não"
                    );

                    if (confirm)
                    {
                        var db = BancoDeDados.Database;
                        bool sucesso = await db.ExcluirCelularAsync(celular);

                        if (sucesso)
                        {
                            await DisplayAlert("Sucesso !!!!", "", "OK");
                            await refCarregando();
                        }
                        else
                        {
                            await DisplayAlert("Erro !!!!", "Falha ao excluir celular", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro !!!!", $"Erro ao excluir: {ex.Message}", "OK");
            }
        }

        // Evento do ContextAction "Excluir Celular"
        private async void OnExcluirContextAction(object sender, EventArgs e)
        {
            try
            {
                if (sender is MenuItem menuItem && menuItem.CommandParameter is Celular celular)
                {
                    bool confirm = await DisplayAlert(
                        "Confirmar !!!!",
                        $"Deseja excluir {celular.celFabricante} {celular.celModelo}?",
                        "Sim",
                        "Não"
                    );

                    if (confirm)
                    {
                        var db = BancoDeDados.Database;
                        bool sucesso = await db.ExcluirCelularAsync(celular);

                        if (sucesso)
                        {
                            await DisplayAlert("Sucesso !!!!", "", "OK");
                            await refCarregando();
                        }
                        else
                        {
                            await DisplayAlert("Erro !!!!", "Falha ao excluir celular", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro !!!!", $"Erro ao excluir: {ex.Message}", "OK");
            }
        }
    }
}
```

## ARQUIVO 10: Views/TelaIncluirCelular.xaml
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="appProvaA1Celular.Views.TelaIncluirCelular"
             Title="Incluir Celular">

    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Salvar"
                     IconImageSource="salvarcelular.png"
                     Clicked="ToolbarItemClickedSalvar"
                     SemanticProperties.Hint="Salvar este celular no sistema"
                     Order="Primary" />
    </ContentPage.ToolbarItems>

    <ContentPage.Content>
        <StackLayout Padding="20" Spacing="15">

            <!-- Título -->
            <Label Text="Cadastrar Novo Celular"
                   FontSize="28"
                   FontAttributes="Bold"
                   HorizontalTextAlignment="Center"
                   Margin="0,0,0,20" />

            <!-- Campo Fabricante -->
            <Border Stroke="#512BD4" Padding="10,5" Margin="4" StrokeShape="RoundRectangle 8,0">
                <Entry Placeholder="Fabricante:"
                       x:Name="txtFabricante"
                       FontAttributes="Bold"
                       Margin="4"
                       ClearButtonVisibility="WhileEditing" />
            </Border>

            <!-- Campo Modelo -->
            <Border Stroke="#512BD4" Padding="10,5" Margin="4" StrokeShape="RoundRectangle 8,0">
                <Entry Placeholder="Modelo:"
                       x:Name="txtModelo"
                       FontAttributes="Bold"
                       Margin="4"
                       ClearButtonVisibility="WhileEditing" />
            </Border>

            <!-- Botões -->
            <Grid ColumnDefinitions="*,*" RowDefinitions="Auto" Margin="0,20,0,0">
                <Button Grid.Column="0"
                        Text="Cancelar"
                        Clicked="OnCancelarClicked"
                        HeightRequest="55"
                        CornerRadius="10"
                        BackgroundColor="Gray"
                        TextColor="White"
                        Margin="0,0,10,0" />

                <Button Grid.Column="1"
                        Text="Salvar"
                        Clicked="OnSalvarClicked"
                        HeightRequest="55"
                        CornerRadius="10"
                        BackgroundColor="#512BD4"
                        TextColor="White"
                        Margin="10,0,0,0" />
            </Grid>

        </StackLayout>
    </ContentPage.Content>

</ContentPage>
```

## ARQUIVO 11: Views/TelaIncluirCelular.xaml.cs
```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using appProvaA1Celular.Model;
using appProvaA1Celular.DAL;

namespace appProvaA1Celular.Views
{
    public partial class TelaIncluirCelular : ContentPage
    {
        public TelaIncluirCelular()
        {
            InitializeComponent();
        }

        // Trata o evento Clicked do ToolbarItem Salvar (OBRIGATÓRIO segundo Apostila 09)
        private void ToolbarItemClickedSalvar(object sender, EventArgs e)
        {
            OnSalvarClicked(sender, e);
        }

        // Trata o evento Clicked do botão Salvar
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                // Verificando se os elementos Entry estão vazios ou nulos
                if (string.IsNullOrWhiteSpace(txtFabricante.Text))
                {
                    await DisplayAlert("Erro !!!!", "Verifique se a caixa de texto Fabricante está vazia !!!!", "OK");
                    txtFabricante.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtModelo.Text))
                {
                    await DisplayAlert("Erro !!!!", "Verifique se a caixa de texto Modelo está vazia !!!!", "OK");
                    txtModelo.Focus();
                    return;
                }
                else
                {
                    // Preenchendo o model Celular com os dados informados na interface gráfica.
                    var celular = new Celular
                    {
                        celFabricante = txtFabricante.Text.Trim(),
                        celModelo = txtModelo.Text.Trim()
                    };

                    var db = BancoDeDados.Database;
                    bool sucesso = await db.IncluirCelularAsync(celular);

                    if (sucesso)
                    {
                        await DisplayAlert("Sucesso !!!!", "Celular cadastrado com sucesso !!!!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Erro !!!!", "Falha ao incluir celular", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro !!!!", $"Erro ao salvar: {ex.Message}", "OK");
            }
        }

        // Trata o evento Clicked do botão Cancelar
        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
```

## ARQUIVO 12: Views/TelaAlterarCelular.xaml
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="appProvaA1Celular.Views.TelaAlterarCelular"
             Title="Alterar Celular">

    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Salvar"
                     IconImageSource="salvarcelular.png"
                     Clicked="ToolbarItemClickedSalvar"
                     SemanticProperties.Hint="Salvar alterações deste celular"
                     Order="Primary" />
        <ToolbarItem Text="Excluir"
                     IconImageSource="excluircelular.png"
                     Clicked="OnExcluirClicked"
                     SemanticProperties.Hint="Excluir este celular permanentemente"
                     Order="Primary" />
    </ContentPage.ToolbarItems>

    <ContentPage.Content>
        <StackLayout Padding="20" Spacing="15">

            <!-- Título -->
            <Label Text="Alterar Celular"
                   FontSize="28"
                   FontAttributes="Bold"
                   HorizontalTextAlignment="Center"
                   Margin="0,0,0,20" />

            <!-- Campo Fabricante -->
            <Border Stroke="#512BD4" Padding="10,5" Margin="4" StrokeShape="RoundRectangle 8,0">
                <Entry Placeholder="Fabricante:"
                       x:Name="txtFabricante"
                       FontAttributes="Bold"
                       Margin="4"
                       ClearButtonVisibility="WhileEditing" />
            </Border>

            <!-- Campo Modelo -->
            <Border Stroke="#512BD4" Padding="10,5" Margin="4" StrokeShape="RoundRectangle 8,0">
                <Entry Placeholder="Modelo:"
                       x:Name="txtModelo"
                       FontAttributes="Bold"
                       Margin="4"
                       ClearButtonVisibility="WhileEditing" />
            </Border>

            <!-- Botões -->
            <Grid ColumnDefinitions="*,*,*" RowDefinitions="Auto" Margin="0,20,0,0">
                <Button Grid.Column="0"
                        Text="Cancelar"
                        Clicked="OnCancelarClicked"
                        HeightRequest="55"
                        CornerRadius="10"
                        BackgroundColor="Gray"
                        TextColor="White"
                        Margin="0,0,10,0" />

                <Button Grid.Column="1"
                        Text="Salvar"
                        Clicked="OnSalvarClicked"
                        HeightRequest="55"
                        CornerRadius="10"
                        BackgroundColor="#512BD4"
                        TextColor="White"
                        Margin="10,0,10,0" />

                <Button Grid.Column="2"
                        Text="Excluir"
                        Clicked="OnExcluirClicked"
                        HeightRequest="55"
                        CornerRadius="10"
                        BackgroundColor="Red"
                        TextColor="White"
                        Margin="10,0,0,0" />
            </Grid>

        </StackLayout>
    </ContentPage.Content>

</ContentPage>
```

## ARQUIVO 13: Views/TelaAlterarCelular.xaml.cs
```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using appProvaA1Celular.Model;
using appProvaA1Celular.DAL;

namespace appProvaA1Celular.Views
{
    public partial class TelaAlterarCelular : ContentPage
    {
        private Celular _celularAnexada;

        public TelaAlterarCelular()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Obtém qual foi a Celular anexada no BindingContext da página no momento
            // que ela foi criada e enviada para a navegação.
            _celularAnexada = BindingContext as Celular;

            if (_celularAnexada != null)
            {
                txtFabricante.Text = _celularAnexada.celFabricante;
                txtModelo.Text = _celularAnexada.celModelo;
            }
        }

        // Trata o evento Clicked do ToolbarItem Salvar (OBRIGATÓRIO segundo Apostila 09)
        private void ToolbarItemClickedSalvar(object sender, EventArgs e)
        {
            OnSalvarClicked(sender, e);
        }

        // Trata o evento Clicked do botão Salvar
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                // Verificando se os elementos Entry estão vazios ou nulos
                if (string.IsNullOrWhiteSpace(txtFabricante.Text))
                {
                    await DisplayAlert("Erro !!!!", "Verifique se a caixa de texto Fabricante está vazia !!!!", "OK");
                    txtFabricante.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtModelo.Text))
                {
                    await DisplayAlert("Erro !!!!", "Verifique se a caixa de texto Modelo está vazia !!!!", "OK");
                    txtModelo.Focus();
                    return;
                }
                else
                {
                    if (_celularAnexada == null)
                    {
                        await DisplayAlert("Erro !!!!", "Celular não encontrado", "OK");
                        return;
                    }

                    // Preenchendo o model Celular com os dados informados na interface gráfica
                    _celularAnexada.celFabricante = txtFabricante.Text.Trim();
                    _celularAnexada.celModelo = txtModelo.Text.Trim();

                    var db = BancoDeDados.Database;
                    bool sucesso = await db.AlterarCelularAsync(_celularAnexada);

                    if (sucesso)
                    {
                        await DisplayAlert("Sucesso !!!!", "Celular alterado com sucesso !!!!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Erro !!!!", "Falha ao alterar celular", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro !!!!", $"Erro ao salvar: {ex.Message}", "OK");
            }
        }

        // Trata o evento Clicked do botão Cancelar
        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // Trata o evento Clicked do ToolbarItem Excluir (CRUD completo conforme Apostila 08/09)
        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (_celularAnexada == null)
            {
                await DisplayAlert("Erro !!!!", "Celular não encontrado", "OK");
                return;
            }

            bool confirmar = await DisplayAlert("Confirmar !!!!", "Deseja realmente excluir este celular?", "Sim", "Não");
            if (!confirmar)
                return;

            var db = BancoDeDados.Database;
            bool sucesso = await db.ExcluirCelularAsync(_celularAnexada);

            if (sucesso)
            {
                await DisplayAlert("Sucesso !!!!", "Celular excluído com sucesso !!!!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro !!!!", "Falha ao excluir celular", "OK");
            }
        }
    }
}
```

## FIM DO CÓDIGO COMPLETO PARA ANÁLISE DO PROFESSOR
