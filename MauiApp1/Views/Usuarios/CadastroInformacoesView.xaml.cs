using MauiApp1.ViewModels.Usuarios;

namespace MauiApp1.Views.Usuarios;

public partial class CadastroInformacoesView : ContentPage
{
    CadastroInformacoesViewModel viewModel;

    public CadastroInformacoesView()
	{
        InitializeComponent();
        viewModel = new CadastroInformacoesViewModel();
        BindingContext = viewModel;
    }
}