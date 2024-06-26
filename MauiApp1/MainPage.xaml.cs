using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private AuthService authService;


        public MainPage()
        {
            InitializeComponent();
            SetGreeting();
            BindingContext = this;
            authService = new AuthService();
            authService.OnAuthStateChanged += AuthStateChanged;
        }

        private void SetGreeting()
        {
            string username = Preferences.Get("UsuarioUsername", "Usuário");

            string greeting;
            int hour = DateTime.Now.Hour;

            if (hour < 12)
            {
                greeting = "Bom dia";
            }
            else if (hour < 18)
            {
                greeting = "Boa tarde";
            }
            else
            {
                greeting = "Boa noite";
            }

            GreetingLabel.Text = $"{greeting}, {username}!";
        }

        private void AuthStateChanged(object sender, bool isAuthenticated)
        {
            
        }

    }
}
