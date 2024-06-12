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
            aService = new AguaService(token);

            SalvarCommand = new Command(async () => { await SalvarAgua(); });
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        #region Atributos
        private int id;
        private double consumo;
        private TimeOnly timeonly;

        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public double Consumo { get => consumo; set { consumo = value; OnPropertyChanged(nameof(Consumo)); } }
        public TimeOnly timeOnly { get => timeonly; set { timeonly = value; OnPropertyChanged(nameof(TimeOnly)); } }

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
                Agua agua = new Agua()
                {
                    Id = id,
                    Consumo = consumo,
                    timeOnly = timeOnly,
                    
                };

                if (agua.Id == 0)
                    await aService.PostAguaAsync(agua);
                else
                    await aService.PutAguaAsync(agua);

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

        public async void CarregarAgua()
        {
            try
            {
                Agua a = await
                aService.GetAguaAsync(int.Parse(aguaSelecionadoId));
                this.Id = a.Id;
                this.Consumo = a.Consumo;
                this.timeOnly = a.timeOnly;
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
