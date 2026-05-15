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
