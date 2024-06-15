using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.Receitas
{
    public class ReceitaService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "";
        private string _token;

        public ReceitaService(string token)
        {
            _request = new Request(); ;
            _token = token;
        }

        public async Task<int> PostReceitaAsync(Receita receita)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, receita, _token);
        }
        public async Task<ObservableCollection<Receita>> GetReceitaAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Receita> listaReceitas = await
                _request.GetAsync<ObservableCollection<Models.Receita>>(apiUrlBase + urlComplementar, _token);
            return listaReceitas;
        }
        public async Task<Receita> GetReceitaAsync(int receitaId)
        {
            string urlComplementar = string.Format("/{0}", receitaId);
            var receita = await _request.GetAsync<Models.Receita>(apiUrlBase + urlComplementar, _token);
            return receita;
        }
        public async Task<int> PutReceitaAsync(Receita receita)
        {
            var result = await _request.PutAsync(apiUrlBase, receita, _token);
            return result;
        }
        public async Task<int> DeleteReceitaAsync(int receitaId)
        {
            string urlComplementar = string.Format("/{0}", receitaId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
