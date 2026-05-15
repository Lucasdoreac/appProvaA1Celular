using Android.App;
using Android.Content.PM;
using Microsoft.Maui;

namespace br.edu.udf;

[Activity(Name = "br.edu.udf.MainActivity",
          Theme = "@style/Maui.MainTheme.NoActionBar",
          MainLauncher = true,
          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    // O .NET MAUI usa esta classe para injetar o código de inicialização
}