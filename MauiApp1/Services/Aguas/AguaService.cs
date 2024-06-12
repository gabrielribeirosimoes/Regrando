using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.Aguas
{
    public class AguaService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://rpgapi20241pam.azurewebsites.net/Personagens";
        private string _token;

        public AguaService(string token)
        {
            _request = new Request(); ;
            _token = token;
        }

        public async Task<int> PostAguaAsync(Agua agua)
        {
            return await _request.PostReturnIntAsync(apiUrlBase, agua, _token);
        }
        public async Task<ObservableCollection<Agua>> GetAguaAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Agua> listaAguas = await
                _request.GetAsync<ObservableCollection<Models.Agua>>(apiUrlBase + urlComplementar, _token);
            return listaAguas;
        }
        public async Task<Agua> GetAguaAsync(int aguaId)
        {
            string urlComplementar = string.Format("/{0}", aguaId);
            var agua = await _request.GetAsync<Models.Agua>(apiUrlBase + urlComplementar, _token);
            return agua;
        }
        public async Task<int> PutAguaAsync(Agua agua)
        {
            var result = await _request.PutAsync(apiUrlBase, agua, _token);
            return result;
        }
        public async Task<int> DeleteAguaAsync(int aguaId)
        {
            string urlComplementar = string.Format("/{0}", aguaId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
