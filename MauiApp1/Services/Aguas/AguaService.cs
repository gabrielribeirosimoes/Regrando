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

        public async Task<Agua> GetAguaByIdAsync(int IdAgua)
        {
            Agua agua = null;

            HttpResponseMessage response = await _httpClient.GetAsync($"{BaseUrl}/{IdAgua}");
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
            Agua retornoAgua = JsonConvert.DeserializeObject<Agua>(responseContent);

            return retornoAgua.IdAgua;
        }

        public async Task<bool> PutAguaAsync(int IdAgua, Agua agua)
        {
            string json = JsonConvert.SerializeObject(agua);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{BaseUrl}/{IdAgua}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAguaAsync(int IdAgua)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{BaseUrl}/{IdAgua}");
            return response.IsSuccessStatusCode;
        }
    }
}
