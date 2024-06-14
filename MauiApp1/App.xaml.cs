namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Inicialmente, carrega a página de login
            MainPage = new NavigationPage(new Views.Usuarios.LoginView());
        }

        public async Task NavigateToSecondPageAsync()
        {
            // Navega para a segunda página após a página de login
            await MainPage.Navigation.PushAsync(new Views.Usuarios.CadastroInformacoesView());
        }
    }
}

