using MauiApp1.Models;
using MauiApp1.Models.Enuns;
using MauiApp1.Services.Refeicoes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Refeicoes
{
    [QueryProperty("RefeicaoSelecionadoId", "pId")]
    public class CadastroRefeicaoViewModel : BaseViewModel
    {
        private RefeicaoService rService;

        public CadastroRefeicaoViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            rService = new RefeicaoService(token);
            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarRefeicao(); });
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        #region Atributos
        private int id;
        private string nome;
        private int carboidratos;
        private int gorduras;
        private int proteinas;
        private int calorias;
        private int fibras;
        private int sodio;
        private ObservableCollection<TipoClasse> listaTiposClasse;
        private TipoClasse tipoClasseSelecionado;


        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Nome { get => nome; set { nome = value; OnPropertyChanged(nameof(Nome)); } }
        public int Carboidratos { get => carboidratos; set { carboidratos = value; OnPropertyChanged(nameof(Carboidratos)); } }
        public int Gorduras { get => gorduras; set { gorduras = value; OnPropertyChanged(nameof(Gorduras)); } }
        public int Proteinas { get => proteinas; set { proteinas = value; OnPropertyChanged(nameof(Proteinas)); } }
        public int Calorias { get => calorias; set { calorias = value; OnPropertyChanged(nameof(Calorias)); } }
        public int Fibras { get => fibras; set { fibras = value; OnPropertyChanged(nameof(Fibras)); } }
        public int Sodio { get => sodio; set { sodio = value; OnPropertyChanged(nameof(Sodio)); } }
        public ObservableCollection<TipoClasse> ListaTiposClasse
        {
            get { return listaTiposClasse; }
            set
            {
                if (value != null)
                {
                    listaTiposClasse = value;
                    OnPropertyChanged(nameof(ListaTiposClasse));
                }
            }
        }

        public TipoClasse TipoClasseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged(nameof(ListaTiposClasse));
                }
            }
        }

        private string refeicaoSelecionadoId;
        public string RefeicaoSelecionadoId
        {
            get => refeicaoSelecionadoId;
            set
            {
                refeicaoSelecionadoId = Uri.UnescapeDataString(value);
                CarregarRefeicao();
            }
        }

        #endregion

        #region Commands
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }
        #endregion 

        #region Metodos
        public async Task ObterClasses()
        {
            try
            {
                listaTiposClasse = new ObservableCollection<TipoClasse>();
                ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Café da Manhã" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Almoço" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Café da Tarde" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 4, Descricao = "Janta" });
                OnPropertyChanged(nameof(ListaTiposClasse));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task SalvarRefeicao()
        {
            try
            {
                Refeicao refeicao = new Refeicao()
                {
                    Id = id,
                    Nome = nome,
                    Carboidratos = carboidratos,
                    Gorduras = gorduras,
                    Proteinas = proteinas,
                    Calorias = calorias,
                    Fibras = fibras,
                    Sodio = sodio,
                    Classe = (TipoRefeicaoEnum)tipoClasseSelecionado.Id
                };

                if (refeicao.Id == 0)
                    await rService.PostRefeicaoAsync(refeicao);
                else
                    await rService.PutRefeicaoAsync(refeicao);

                await Application.Current.MainPage
                             .DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async void CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void CarregarRefeicao()
        {
            try
            {
                Refeicao r = await
                rService.GetRefeicaoAsync(int.Parse(refeicaoSelecionadoId));
                this.Id = r.Id;
                this.Nome = r.Nome;
                this.Carboidratos = r.Carboidratos;
                this.Gorduras = r.Gorduras;
                this.Proteinas = r.Proteinas;
                this.Calorias = r.Calorias;
                this.Fibras = r.Fibras;
                this.Sodio = r.Sodio;

                TipoClasseSelecionado = ListaTiposClasse
                    .FirstOrDefault(tClasse => tClasse.Id == (int)r.Classe);
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
