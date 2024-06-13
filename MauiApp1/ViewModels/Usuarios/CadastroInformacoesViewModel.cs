using MauiApp1.Models;
using MauiApp1.Services.Usuarios;
using MauiApp1.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Usuarios
{
    public class CadastroInformacoesViewModel : BaseViewModel
    {


        #region AtributosPropriedades

        public ObservableCollection<TipoMetas> ListaTiposMetas;
        public ObservableCollection<TipoSexo> ListaTiposSexo;
        private int idade = 0;
        private double altura = 0;
        private double peso = 0;
        private TipoMetas tipoMetasSelecionado;


        public int Idade
        {
            get => idade;
            set
            {
                idade = value;
                OnPropertyChanged(nameof(Idade));
            }
        }

        public double Altura
        {
            get => altura;
            set
            {
                altura = value;
                OnPropertyChanged(nameof(altura));
            }
        }

        public double Peso
        {
            get => peso;
            set
            {
                peso = value;
                OnPropertyChanged(nameof(Peso));
            }
        }
        

        public TipoMetas TipoMetasSelecionado
        {
            get { return tipoMetasSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoMetasSelecionado = value;
                    OnPropertyChanged(nameof(ListaTiposMetas));
                }
            }
        }
        #endregion

        #region
        public async Task ObterClasses()
        {
            try
            {
                ListaTiposMetas.Add(new TipoMetas() { Id = 1, Descricao = "Perder peso" });
                ListaTiposMetas.Add(new TipoMetas() { Id = 2, Descricao = "Manter peso" });
                ListaTiposMetas.Add(new TipoMetas() { Id = 3, Descricao = "Ganhar peso" });
                OnPropertyChanged(nameof(ListaTiposMetas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion


    }
}
