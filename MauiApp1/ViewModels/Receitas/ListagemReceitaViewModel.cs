using MauiApp1.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels.Receitas
{
    public class ListagemReceitaViewModel : BaseViewModel
    {
        public ObservableCollection<Receita> Receitas { get; set; }

        public ListagemReceitaViewModel()
        {
            Receitas = new ObservableCollection<Receita>();
            AdicionarReceitasExemplo();
        }

        private void AdicionarReceitasExemplo()
        {
            Receitas.Add(new Receita()
            {
                IdReceita = 1,
                NomeReceita = "Lasanha à Bolonhesa",
                Descricao = "Deliciosa lasanha de carne moída com molho bolonhesa.",
                Ingredientes = "Ingredientes: Massa de lasanha, carne moída, molho de tomate, queijo, etc.",
                ModoPreparo = "Modo de Preparo: Cozinhe a massa, prepare o molho bolonhesa, monte as camadas e asse no forno.",
                Calorias = 500,
                TempoPreparoMinutos = 60
            });

            Receitas.Add(new Receita()
            {
                IdReceita = 2,
                NomeReceita = "Frango Assado",
                Descricao = "Frango marinado e assado lentamente no forno.",
                Ingredientes = "Ingredientes: Frango, limão, alho, alecrim, sal, pimenta, etc.",
                ModoPreparo = "Modo de Preparo: Marine o frango, asse lentamente no forno até dourar.",
                Calorias = 400,
                TempoPreparoMinutos = 75
            });

            Receitas.Add(new Receita()
            {
                IdReceita = 3,
                NomeReceita = "Risoto de Cogumelos",
                Descricao = "Risoto cremoso com cogumelos frescos.",
                Ingredientes = "Ingredientes: Arroz arbóreo, cogumelos frescos, caldo de legumes, queijo parmesão, etc.",
                ModoPreparo = "Modo de Preparo: Refogue os cogumelos, adicione o arroz, cozinhe gradualmente com caldo até ficar cremoso.",
                Calorias = 450,
                TempoPreparoMinutos = 45
            });

            Receitas.Add(new Receita()
            {
                IdReceita = 4,
                NomeReceita = "Salada Caesar",
                Descricao = "Salada fresca com molho Caesar caseiro.",
                Ingredientes = "Ingredientes: Alface romana, croutons, queijo parmesão, molho Caesar (ovo, azeite, anchovas, etc.).",
                ModoPreparo = "Modo de Preparo: Misture todos os ingredientes e adicione o molho Caesar no final.",
                Calorias = 300,
                TempoPreparoMinutos = 15
            });

            Receitas.Add(new Receita()
            {
                IdReceita = 5,
                NomeReceita = "Brownie de Chocolate",
                Descricao = "Brownie denso e fudgy com pedaços de chocolate.",
                Ingredientes = "Ingredientes: Chocolate meio amargo, manteiga, açúcar, ovos, farinha de trigo, fermento, etc.",
                ModoPreparo = "Modo de Preparo: Derreta o chocolate e a manteiga, misture com os outros ingredientes, asse até ficar firme por fora e macio por dentro.",
                Calorias = 350,
                TempoPreparoMinutos = 40
            });
        }
    }
}
