﻿using MauiApp1.Models;
using MauiApp1.Services.Usuarios;
using MauiApp1.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService _uService;

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

        #region
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
                OnPropertyChanged(Nome);
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(Login);
            }
        }

        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged(Senha);
            }
        }
        #endregion

        #region
        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nome = Nome;
                usuario.Username = Login;
                usuario.PasswordString = Senha;

                Usuario usuarioAutenticado = await _uService.PostAutenticarUsuarioAsync(usuario);

                if (!string.IsNullOrEmpty(usuarioAutenticado.Token))
                {
                    string menssagem = $"Bem vindo(a) {usuarioAutenticado.Username}";

                    Preferences.Set("UsuarioId", usuarioAutenticado.Id);
                    Preferences.Set("UsuarioUsername", usuarioAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", usuarioAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", usuarioAutenticado.Token);

                    await Application.Current.MainPage
                        .DisplayAlert("Informações", menssagem, "Ok");

                    Application.Current.MainPage = new AppShell();

                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nome = Nome;
                usuario.Username = Login;
                usuario.PasswordString = Senha;

                Usuario usuarioRegistrado = new Usuario();// await _uService.PostRegistrarUsuarioAsync(usuario);
                usuarioRegistrado.Id = 1;

                if (usuarioRegistrado.Id != 0)
                {
                    string mensagem = $"Usuario Id: {usuarioRegistrado.Id} registrado com sucesso";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new NavigationPage(new Views.Usuarios.CadastroInformacoesView());
                }



                










            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "OK");
            }
        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage
                         .Navigation.PushAsync(new CadastroView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "OK");
            }
        }
        #endregion
    }
}



