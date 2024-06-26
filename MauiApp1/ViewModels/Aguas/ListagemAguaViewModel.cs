using MauiApp1.Models;
using MauiApp1.Services.Aguas;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MauiApp1.ViewModels.Aguas
{
    public class ListagemAguaViewModel : BaseViewModel
    {
        private readonly AguaService _aService;
        private Agua _aguaSelecionado;

        public ObservableCollection<Agua> Aguas { get; set; }
        public ICommand NovaAgua { get; }
        public ICommand RemoverAguaCommand { get; }

        public ListagemAguaViewModel()
        {
            _aService = new AguaService();
            Aguas = new ObservableCollection<Agua>();
            _ = ObterAguas();

            NovaAgua = new Command(async () => await ExibirCadastroAgua());
            RemoverAguaCommand = new Command<Agua>(async (a) => await RemoverAgua(a));
        }

        public Agua AguaSelecionado
        {
            get => _aguaSelecionado;
            set
            {
                if (value != null)
                {
                    _aguaSelecionado = value;
                    OnPropertyChanged();
                    _ = Shell.Current.GoToAsync($"cadAguaView?pId={_aguaSelecionado.IdAgua}");
                }
            }
        }

        public async Task ObterAguas()
        {
            try
            {
                var aguas = await _aService.GetAguasAsync();
                if (aguas != null)
                {
                    Aguas.Clear();
                    foreach (var agua in aguas)
                    {
                        Aguas.Add(agua);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException?.Message, "Ok");
            }
        }

        public async Task ExibirCadastroAgua()
        {
            try
            {
                await Shell.Current.GoToAsync("cadAguaView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException?.Message, "Ok");
            }
        }

        public async Task RemoverAgua(Agua a)
        {
            try
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja realmente excluir a água?", "Sim", "Não");
                if (confirm)
                {
                    await _aService.DeleteAguaAsync(a.IdAgua);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Água excluída com sucesso", "Ok");
                    await ObterAguas();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException?.Message, "Ok");
            }
        }
    }
}
