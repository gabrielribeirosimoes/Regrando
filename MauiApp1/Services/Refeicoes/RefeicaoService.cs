using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.Refeicoes
{
    public class RefeicaoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://rpgapi20241pam.azurewebsites.net/Personagens";
        private string _token;

        public RefeicaoService(string token)
        {
            _request = new Request(); ;
            _token = token;
        }

        public async Task<int> PostRefeicaoAsync(Refeicao refeicao)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, refeicao, _token);
        }
        public async Task<ObservableCollection<Refeicao>> GetRefeicaoAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Refeicao> listaRefeicoes = await
                _request.GetAsync<ObservableCollection<Models.Refeicao>>(apiUrlBase + urlComplementar, _token);
            return listaRefeicoes;
        }
        public async Task<Refeicao> GetRefeicaoAsync(int refeicaoId)
        {
            string urlComplementar = string.Format("/{0}", refeicaoId);
            var refeicao = await _request.GetAsync<Models.Refeicao>(apiUrlBase + urlComplementar, _token);
            return refeicao;
        }
        public async Task<int> PutRefeicaoAsync(Refeicao refeicao)
        {
            var result = await _request.PutAsync(apiUrlBase, refeicao, _token);
            return result;
        }
        public async Task<int> DeleteRefeicaoAsync(int refeicaoId)
        {
            string urlComplementar = string.Format("/{0}", refeicaoId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
