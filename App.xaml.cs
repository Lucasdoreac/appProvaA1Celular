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