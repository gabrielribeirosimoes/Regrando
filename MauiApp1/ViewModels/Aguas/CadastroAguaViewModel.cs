using MauiApp1.Models;
using MauiApp1.Services.Aguas;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Aguas
{
    [QueryProperty("AguaSelecionadoId", "aId")]
    public class CadastroAguaViewModel : BaseViewModel
    {
        private AguaService aService;

        public CadastroAguaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            aService = new AguaService();

            SalvarCommand = new Command(async () => { await SalvarAgua(); });
            CancelarCommand = new Command(async () => { await CancelarCadastro(); });
            AtualizarUltimoConsumo(); 
            CarregarAguas(); 
        }

        #region Atributos
        private int idAgua;
        private int? qtdAgua;

        public int IdAgua { get => idAgua; set { idAgua = value; OnPropertyChanged(nameof(IdAgua)); } }
        public int? QtdAgua { get => qtdAgua; set { qtdAgua = value; OnPropertyChanged(nameof(QtdAgua)); } }

        private string aguaSelecionadoId;
        public string AguaSelecionadoId
        {
            get => aguaSelecionadoId;
            set
            {
                aguaSelecionadoId = Uri.UnescapeDataString(value);
                CarregarAgua();
            }
        }

        private string ultimoConsumo;
        public string UltimoConsumo
        {
            get => ultimoConsumo;
            set { ultimoConsumo = value; OnPropertyChanged(nameof(UltimoConsumo)); }
        }

        private ObservableCollection<Agua> aguas;
        public ObservableCollection<Agua> Aguas
        {
            get => aguas;
            set { aguas = value; OnPropertyChanged(nameof(Aguas)); }
        }
        #endregion

        #region Commands
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }
        #endregion

        #region Metodos
        public async Task SalvarAgua()
        {
            try
            {
                Agua agua = new Agua
                {
                    IdAgua = idAgua,
                    QtdAgua = qtdAgua
                };

                if (agua.IdAgua == 0)
                    await aService.PostAguaAsync(agua);
                else
                    await aService.PutAguaAsync(agua.IdAgua, agua);

                await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
                await Shell.Current.GoToAsync("..");

                AtualizarUltimoConsumo(); 
                CarregarAguas(); 
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async Task CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void CarregarAgua()
        {
            try
            {
                Agua agua = await aService.GetAguaByIdAsync(int.Parse(aguaSelecionadoId));
                this.IdAgua = agua.IdAgua;
                this.QtdAgua = agua.QtdAgua;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async void AtualizarUltimoConsumo()
        {
            try
            {
                var aguas = await aService.GetAguasAsync();
                var ultimo = aguas.OrderByDescending(a => a.IdAgua).FirstOrDefault(); 
                if (ultimo != null)
                {
                    UltimoConsumo = $"Último Consumo: {ultimo.QtdAgua} ml";
                }
                else
                {
                    UltimoConsumo = "Nenhum consumo registrado";
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private async void CarregarAguas()
        {
            try
            {
                var aguas = await aService.GetAguasAsync();
                Aguas = new ObservableCollection<Agua>(aguas);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
