using MauiApp1.ViewModels.Usuarios;

namespace MauiApp1.Views.Usuarios;

public partial class CadastroView : ContentPage
{
	UsuarioViewModel viewModel;

	public CadastroView()
	{
		InitializeComponent();
		viewModel = new UsuarioViewModel();
		BindingContext = viewModel;
	}
}