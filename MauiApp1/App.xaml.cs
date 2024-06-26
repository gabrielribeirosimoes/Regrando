namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.Usuarios.LoginView());
        }

        public async Task NavigateToSecondPageAsync()
        {
            await MainPage.Navigation.PushAsync(new Views.Usuarios.CadastroInformacoesView());
        }
    }
}

