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
            // Exemplo: Obter dados reais do usu�rio
            string profileImageUrl = "https://example.com/user/profile.jpg"; // URL da imagem de perfil
            string userName = "Jo�o da Silva"; // Nome do usu�rio
            string email = "joao.silva@example.com"; // Email do usu�rio
            string phone = "+55 11 98765-4321"; // Telefone do usu�rio

            // Carregar dados na UI
            ProfileImage.Source = new UriImageSource { Uri = new Uri(profileImageUrl) };
            UserNameLabel.Text = userName;
            EmailLabel.Text = email;
            PhoneLabel.Text = phone;
        }

        private void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Implementar a l�gica para editar o perfil aqui
            // Exemplo:
            DisplayAlert("Editar Perfil", "Funcionalidade de editar perfil n�o implementada ainda.", "OK");
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Implementar a l�gica para logout aqui
            // Exemplo:
            DisplayAlert("Logout", "Voc� foi deslogado.", "OK");
            // Exemplo: Navegar para a p�gina de login
            Application.Current.MainPage = new LoginView();
        }
    }
}
