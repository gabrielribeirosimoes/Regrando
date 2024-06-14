using MauiApp1.ViewModels.Receitas;

namespace MauiApp1.Views.Receitas;

public partial class ListagemView : ContentPage
{
    ListagemReceitaViewModel viewModel;
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
        _ = viewModel.ObterReceitas();
    }
}