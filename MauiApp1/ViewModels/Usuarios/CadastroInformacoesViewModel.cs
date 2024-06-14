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
        private UsuarioService _uService;

        public CadastroInformacoesViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
        }

        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }
        #region AtributosPropriedades

        public ObservableCollection<TipoMetas> ListaTiposMetas;
        public ObservableCollection<TipoSexo> ListaTiposSexo;
        private int idade = 0;
        private double altura = 0;
        private double peso = 0;
        private TipoMetas tipoMetasSelecionado;
        private TipoSexo tipoSexoSelecionado;


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

        public TipoSexo TipoSexoSelecionado
        {
            get { return tipoSexoSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoSexoSelecionado = value;
                    OnPropertyChanged(nameof(ListaTiposSexo));
                }
            }
        }
        #endregion

        #region Metódos
        public async Task ObterMetas()
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

        public async Task ObterSexo()
        {
            try
            {
                ListaTiposSexo.Add(new TipoSexo() { Id = 1, Descricao = "Masculino" });
                ListaTiposSexo.Add(new TipoSexo() { Id = 2, Descricao = "Feminino" });
                OnPropertyChanged(nameof(ListaTiposSexo));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                TipoMetas tipoMetas = new TipoMetas();
                tipoMetas.Id = Idade;

                TipoMetas tipoMetasRegistrado = new TipoMetas();// await _uService.PostRegistrarTipoMetasAsync(tipoMetas);
                tipoMetasRegistrado.Id = 1;

                if (tipoMetasRegistrado.Id != 0)
                {
                    string mensagem = $"Usuario Id: {tipoMetasRegistrado.Id} registrado com sucesso";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new NavigationPage(new Views.Usuarios.LoginView());
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
