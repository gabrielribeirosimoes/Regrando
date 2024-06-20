using MauiApp1.Models;
using MauiApp1.Services.Aguas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        #region Atributos
        private int idAgua;
        private int? qtdAgua;
        private TimeSpan? hrAgua;

        public int IdAgua { get => idAgua; set { idAgua = value; OnPropertyChanged(nameof(IdAgua)); } }
        public int? QtdAgua { get => qtdAgua; set { qtdAgua = value; OnPropertyChanged(nameof(QtdAgua)); } }
        public TimeSpan? HrAgua { get => hrAgua; set { hrAgua = value; OnPropertyChanged(nameof(HrAgua)); } }

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
                Agua agua = new Agua();

                agua.IdAgua = idAgua;
                agua.QtdAgua = qtdAgua;
                agua.HrAgua = DateTime.Now.TimeOfDay;
                

                if (agua.IdAgua == 0)
                    await aService.PostAguaAsync(agua);
                else
                    await aService.PutAguaAsync(agua.IdAgua, agua);

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
                this.HrAgua = agua.HrAgua;
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