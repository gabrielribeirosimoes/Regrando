using System;
using Microsoft.Maui.Controls;

namespace MauiApp1.Views.Usuarios
{
    public partial class ListagemView : ContentPage
    {
        public ListagemView()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            // Exemplo: Obter dados reais do usuário
            string profileImageUrl = "https://example.com/user/profile.jpg"; // URL da imagem de perfil
            string userName = "João da Silva"; // Nome do usuário
            string email = "joao.silva@example.com"; // Email do usuário
            string phone = "+55 11 98765-4321"; // Telefone do usuário

            // Carregar dados na UI
            ProfileImage.Source = new UriImageSource { Uri = new Uri(profileImageUrl) };
            UserNameLabel.Text = userName;
            EmailLabel.Text = email;
            PhoneLabel.Text = phone;
        }

        private void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Implementar a lógica para editar o perfil aqui
            // Exemplo:
            DisplayAlert("Editar Perfil", "Funcionalidade de editar perfil não implementada ainda.", "OK");
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Implementar a lógica para logout aqui
            // Exemplo:
            DisplayAlert("Logout", "Você foi deslogado.", "OK");
            // Exemplo: Navegar para a página de login
            Application.Current.MainPage = new LoginView();
        }
    }
}
