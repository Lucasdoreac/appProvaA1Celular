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
        private System.Collections.Generic.List<Celular> _todosCelulares = new System.Collections.Generic.List<Celular>();

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
                _todosCelulares = celulares.ToList(); // Armazenar lista completa
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
