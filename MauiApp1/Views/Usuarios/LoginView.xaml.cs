using MauiApp1.ViewModels.Usuarios;

namespace MauiApp1.Views.Usuarios;

public partial class LoginView : ContentPage
{
		UsuarioViewModel usuarioViewModel;

		public LoginView()
		{ 
		InitializeComponent();
		usuarioViewModel = new UsuarioViewModel();
		BindingContext = usuarioViewModel;
        }
    
}