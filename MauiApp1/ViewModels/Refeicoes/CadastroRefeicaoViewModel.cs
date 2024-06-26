using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiApp1.Models;
using MauiApp1.Models.Enuns;
using MauiApp1.Services.Alimentos;
using MauiApp1.Services.Refeicoes;
using Newtonsoft.Json;

namespace MauiApp1.ViewModels.Refeicoes
{
    [QueryProperty("RefeicaoSelecionadoId", "pId")]
    public class CadastroRefeicaoViewModel : BaseViewModel
    {
        private readonly RefeicaoService rService;
        private readonly AlimentoService alimentoService;

        public CadastroRefeicaoViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            rService = new RefeicaoService(token);
            alimentoService = new AlimentoService();

            _ = ObterClasses();

            Alimentos = new ObservableCollection<Alimento>();
            AdicionarAlimentoCommand = new Command<Alimento>(AdicionarAlimento);
            SalvarCommand = new Command(async () => await SalvarRefeicao());
            CancelarCommand = new Command(async () => await CancelarCadastro());
        }

        #region Propriedades

        private int idRefeicao;
        public int IdRefeicao
        {
            get => idRefeicao;
            set { idRefeicao = value; OnPropertyChanged(nameof(IdRefeicao)); }
        }

        private int idUsuario;
        public int IdUsuario
        {
            get => idUsuario;
            set { idUsuario = value; OnPropertyChanged(nameof(IdUsuario)); }
        }

        private TipoRefeicaoEnum tpRefeicao;
        public TipoRefeicaoEnum TpRefeicao
        {
            get => tpRefeicao;
            set { tpRefeicao = value; OnPropertyChanged(nameof(TpRefeicao)); }
        }

        private ObservableCollection<TipoClasse> listaTiposClasse;
        public ObservableCollection<TipoClasse> ListaTiposClasse
        {
            get => listaTiposClasse;
            set
            {
                listaTiposClasse = value;
                OnPropertyChanged(nameof(ListaTiposClasse));
            }
        }

        private TipoClasse tipoClasseSelecionado;
        public TipoClasse TipoClasseSelecionado
        {
            get => tipoClasseSelecionado;
            set
            {
                tipoClasseSelecionado = value;
                OnPropertyChanged(nameof(TipoClasseSelecionado));
            }
        }

        private ObservableCollection<Alimento> alimentos;
        public ObservableCollection<Alimento> Alimentos
        {
            get => alimentos;
            set
            {
                alimentos = value;
                OnPropertyChanged(nameof(Alimentos));
            }
        }

        #endregion

        #region Comandos

        public ICommand AdicionarAlimentoCommand { get; }
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        #endregion

        #region Métodos

        public async Task ObterClasses()
        {
            try
            {
                ListaTiposClasse = new ObservableCollection<TipoClasse>
                {
                    new TipoClasse { Id = 1, Descricao = "Café da Manhã" },
                    new TipoClasse { Id = 2, Descricao = "Almoço" },
                    new TipoClasse { Id = 3, Descricao = "Café da Tarde" },
                    new TipoClasse { Id = 4, Descricao = "Janta" }
                };
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        public async Task SalvarRefeicao()
        {
            try
            {
                Refeicao refeicao = new Refeicao
                {
                    IdRefeicao = IdRefeicao,
                    IdUsuario = IdUsuario,
                    TpRefeicao = TpRefeicao
                };

                foreach (var alimento in Alimentos)
                {
                    refeicao.IdAlimento = alimento.IdAlimento; 
                    if (refeicao.IdRefeicao == 0)
                        await rService.PostRefeicaoAsync(refeicao);
                    else
                        await rService.PutRefeicaoAsync(refeicao);
                }

                await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private async Task CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task BuscarAlimentos(string termoBusca)
        {
            try
            {
                Alimentos.Clear();
                var alimentosEncontrados = await alimentoService.BuscarAlimentos(termoBusca);
                if (alimentosEncontrados != null)
                {
                    foreach (var alimento in alimentosEncontrados)
                    {
                        Alimentos.Add(alimento);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        public void AdicionarAlimento(Alimento alimento)
        {
            Alimentos.Add(alimento);
        }

        #endregion
    }
}
