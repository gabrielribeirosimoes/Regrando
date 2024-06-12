using MauiApp1.Models;
using MauiApp1.Services.Aguas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Aguas
{
    public class ListagemAguaViewModel : BaseViewModel
    {
        private AguaService _aService;

        public ObservableCollection<Agua> Aguas { get; set; }

        public ListagemAguaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            _aService = new AguaService(token);
            Aguas = new ObservableCollection<Agua>();
            _ = ObterAguas();

            NovaAgua = new Command(async () => { await ExibirCadastroAgua(); });
            RemoverAguaCommand = new Command<Agua>(async (a) => { await RemoverAgua(a); });
        }

        public ICommand NovaAgua { get;}
        public ICommand RemoverAguaCommand { get; }

        public async Task ObterAguas()
        {
            try
            {
                Aguas = await _aService.GetAguaAsync();
                OnPropertyChanged(nameof(Aguas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
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
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private Agua _aguaSelecionado;
        public Agua aguaSelecionado
        {
            get => _aguaSelecionado;
            set
            {
                if (value != null)
                {
                    _aguaSelecionado = value;
                    Shell.Current.GoToAsync($"cadAguaView?pId={_aguaSelecionado.Id}");
                }
            }
        }

        public async Task RemoverAgua(Agua a)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja realmente excluir a água?", "Sim", "Não"))
                {
                    await _aService.DeleteAguaAsync(a.Id);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Refeição excluída com sucesso", "Ok");
                    _ = ObterAguas();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
