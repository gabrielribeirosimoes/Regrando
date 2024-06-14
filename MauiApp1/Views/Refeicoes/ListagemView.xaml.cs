using MauiApp1.ViewModels.Refeicoes;

namespace MauiApp1.Views.Refeicoes;

public partial class ListagemView : ContentPage
{	
		ListagemRefeicaoViewModel viewModel;

		public ListagemView()
		{ 
		InitializeComponent();

			viewModel = new ListagemRefeicaoViewModel();
			BindingContext = viewModel;
			Title = "Refeições";
        }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterRefeicoes();
    }
}