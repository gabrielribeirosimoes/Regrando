using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using MauiApp1.Models;
using MauiApp1.Services.Usuarios;
using MauiApp1.Views.Usuarios;

namespace MauiApp1.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private readonly UsuarioService _uService;

        public UsuarioViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
        }

        #region Commands
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }
        #endregion

        #region AtributosPropriedades
        private string nome = string.Empty;
        private string login = string.Empty;
        private string senha = string.Empty;

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged(nameof(Senha));
            }
        }
        #endregion

        #region Métodos
        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nome = Nome,
                    Username = Login,
                    PasswordString = Senha
                };

                Usuario usuarioAutenticado = await _uService.PostAutenticarUsuarioAsync(usuario);

                if (!string.IsNullOrEmpty(usuarioAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {usuarioAutenticado.Username}";

                    Preferences.Set("UsuarioId", usuarioAutenticado.Id);
                    Preferences.Set("UsuarioUsername", usuarioAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", usuarioAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", usuarioAutenticado.Token);

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nome = Nome,
                    Username = Login,
                    PasswordString = Senha
                };

                Usuario usuarioRegistrado = await _uService.PostRegistrarUsuarioAsync(usuario);

                if (usuarioRegistrado.Id != 0)
                {
                    string mensagem = $"Usuario Id: {usuarioRegistrado.Id} registrado com sucesso";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new NavigationPage(new CadastroInformacoesView());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CadastroView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
