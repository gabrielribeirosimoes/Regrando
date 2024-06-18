namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            SetGreeting();

        }
        private void SetGreeting()
        {
            // Aqui, você substituirá "username" pelo nome real do usuário, que você deve obter do login.
            string username = "Usuário"; // Exemplo: obter do login

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
    }
}