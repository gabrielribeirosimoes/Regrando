using MauiApp1.ViewModels.Receitas;
using Microsoft.Maui.Controls;

namespace MauiApp1.Views.Receitas
{
    public partial class ListagemView : ContentPage
    {
        private ListagemReceitaViewModel viewModel;

        public ListagemView()
        {
            InitializeComponent();
            viewModel = new ListagemReceitaViewModel();
            BindingContext = viewModel;
            Title = "Receitas";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //viewModel.ObterReceitas(); 
        }
    }
}
