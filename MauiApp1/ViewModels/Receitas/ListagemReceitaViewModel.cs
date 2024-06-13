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
        }

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

    }
}
