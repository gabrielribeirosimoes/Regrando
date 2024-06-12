using MauiApp1.ViewModels.Aguas;

namespace MauiApp1.Views.Aguas;

public partial class ListagemView : ContentPage
{
	ListagemAguaViewModel viewModel;
	public ListagemView()
	{
		InitializeComponent();
        viewModel = new ListagemAguaViewModel();
        BindingContext = viewModel;
        Title = "Hidrata��o - App Regrando";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterAguas();
    }
}