using MauiApp1.Services.Refeicoes;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Refeicoes
{
    public class ListagemRefeicaoViewModel : BaseViewModel
    {
        private RefeicaoService _rService;

        public ObservableCollection<Refeicao> Refeicoes { get; set; }

        public ListagemRefeicaoViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            _rService = new RefeicaoService(token);
            Refeicoes = new ObservableCollection<Refeicao>();
            _ = ObterRefeicoes();

            NovaRefeicao = new Command(async () => { await ExibirCadastroRefeicao(); });
            RemoverRefeicaoCommand =
                new Command<Refeicao>(async (Refeicao r) => { await RemoverRefeicao(r); });

        }

        public ICommand NovaRefeicao { get; set; }
        public ICommand RemoverRefeicaoCommand { get;}

        public async Task ObterRefeicoes()
        {
            try
            {
                Refeicoes = await _rService.GetRefeicaoAsync();
                OnPropertyChanged(nameof(Refeicoes));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ExibirCadastroRefeicao()
        {
            try
            {
                await Shell.Current.GoToAsync("cadRefeicaoView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private Refeicao _refeicaoSelecionado;
        public Refeicao refeicaoSelecionado
        {
            get => _refeicaoSelecionado;
            set
            {
                if (value != null)
                {
                    _refeicaoSelecionado = value;
                    Shell.Current.GoToAsync($"cadRefeicaoView?pId={_refeicaoSelecionado.IdRefeicao}");
                }
            }
        }

        public async Task RemoverRefeicao(Refeicao p)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja realmente excluir a refeicão?", "Sim", "Não"))
                {
                    await _rService.DeleteRefeicaoAsync(p.IdRefeicao);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Refeição excluída com sucesso", "Ok");
                    _ = ObterRefeicoes();
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
