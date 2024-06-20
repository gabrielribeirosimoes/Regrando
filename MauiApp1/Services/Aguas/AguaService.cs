using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using MauiApp1.Models;
using System.Text;


namespace MauiApp1.Services.Aguas
{
    public class AguaService
    {
        private const string BaseUrl = "http://grsgrsgrs.somee.com/Regrando/Aguas"; 
        private readonly HttpClient _httpClient;

        public AguaService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ObservableCollection<Agua>> GetAguasAsync()
        {
            ObservableCollection<Agua> aguas = null;

            HttpResponseMessage response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                aguas = JsonConvert.DeserializeObject<ObservableCollection<Agua>>(content);
            }

            return aguas;
        }

        public async Task<Agua> GetAguaByIdAsync(int id)
        {
            Agua agua = null;

            HttpResponseMessage response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                agua = JsonConvert.DeserializeObject<Agua>(content);
            }

            return agua;
        }

        public async Task<int> PostAguaAsync(Agua agua)
        {
            string json = JsonConvert.SerializeObject(agua);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(BaseUrl, content);
            response.EnsureSuccessStatusCode(); // Lança exceção em caso de erro

            string responseContent = await response.Content.ReadAsStringAsync();
            int newId = JsonConvert.DeserializeObject<int>(responseContent);

            return newId;
        }

        public async Task<bool> PutAguaAsync(int id, Agua agua)
        {
            string json = JsonConvert.SerializeObject(agua);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAguaAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
