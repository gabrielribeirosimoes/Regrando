using MauiApp1.Views.Aguas;
using MauiApp1.Views.Receitas;
using MauiApp1.Views.Refeicoes;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("cadRefeicaoView", typeof(CadastroRefeicaoView));
            Routing.RegisterRoute("cadAguaView", typeof(CadastroAguaView));
            Routing.RegisterRoute("cadReceitaView", typeof(CadastroReceitaView));

            string login = Preferences.Get("UsuarioUsername", string.Empty);
            lblLogin.Text = $"Login: {login}";
        }
    }
}
