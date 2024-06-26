using MauiApp1.ViewModels.Usuarios;
using Microsoft.Maui.Controls;

namespace MauiApp1.Views.Usuarios
{
    public partial class ListagemView : ContentPage
    {
        public ListagemView()
        {
            InitializeComponent();

            var viewModel = new UsuarioViewModel();

            BindingContext = viewModel;


        }
    }
}
