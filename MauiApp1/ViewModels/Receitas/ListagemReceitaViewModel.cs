using MauiApp1.Models;
using MauiApp1.Services.Receitas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Receitas
{
    public class ListagemReceitaViewModel : BaseViewModel
    {
        private ReceitaService _rService;

        public ObservableCollection<Receita> Receitas { get; set; }

        public ListagemReceitaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            _rService = new ReceitaService(token);
            Receitas = new ObservableCollection<Receita>();
            _ = ObterReceitas();

            NovaReceita = new Command(async () => { await ExibirCadastroReceita(); });
            RemoverReceitaCommand = new Command<Receita>(async (r) => { await RemoverReceita(r); });
        }

        public ICommand NovaReceita { get; }
        public ICommand RemoverReceitaCommand { get; }

        public async Task ObterReceitas()
        {
            try
            {
                Receitas = await _rService.GetReceitaAsync();
                OnPropertyChanged(nameof(Receitas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ExibirCadastroReceita()
        {
            try
            {
                await Shell.Current.GoToAsync("cadReceitaView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private Receita _receitaSelecionado;
        public Receita receitaSelecionado
        {
            get => _receitaSelecionado;
            set
            {
                if (value != null)
                {
                    _receitaSelecionado = value;
                    Shell.Current.GoToAsync($"cadReceitaView?pId={_receitaSelecionado.Id}");
                }
            }
        }

        public async Task RemoverReceita(Receita r)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Atenção", "Deseja realmente excluir a receita?", "Sim", "Não"))
                {
                    await _rService.DeleteReceitaAsync(r.Id);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Receita excluída com sucesso", "Ok");
                    _ = ObterReceitas();
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
