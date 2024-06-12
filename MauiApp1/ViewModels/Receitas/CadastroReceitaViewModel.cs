using MauiApp1.Models;
using MauiApp1.Services.Receitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels.Receitas
{
    [QueryProperty("ReceitaSelecionadoId", "tId")]
    public class CadastroReceitaViewModel : BaseViewModel
    {
        private ReceitaService tService;

        public CadastroReceitaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            tService = new ReceitaService(token);

            SalvarCommand = new Command(async () => { await SalvarReceita(); });
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        #region Atributos
        private int id;
        private string nome;
        private string descricao;

        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Nome { get => nome; set { nome = value; OnPropertyChanged(nameof(Nome)); } }
        public string Descricao { get => descricao; set { descricao = value; OnPropertyChanged(nameof(Descricao)); } }

        private string receitaSelecionadoId;
        public string ReceitaSelecionadoId
        {
            get => receitaSelecionadoId;
            set
            {
                receitaSelecionadoId = Uri.UnescapeDataString(value);
                CarregarReceita();
            }
        }
        #endregion

        #region Commands
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }
        #endregion

        #region Metodos
        public async Task SalvarReceita()
        {
            try
            {
                Receita receita = new Receita()
                {
                    Id = id,
                    Nome = nome,
                    Descricao = descricao,

                };

                if (receita.Id == 0)
                    await tService.PostReceitaAsync(receita);
                else
                    await tService.PutReceitaAsync(receita);

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

        public async void CarregarReceita()
        {
            try
            {
                Receita r = await
                tService.GetReceitaAsync(int.Parse(receitaSelecionadoId));
                this.Id = r.Id;
                this.Nome = r.Nome;
                this.Descricao = r.Descricao;
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
