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
