using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MauiApp1.Models;
using Newtonsoft.Json;

namespace MauiApp1.Services.Alimentos
{
    public class AlimentoService
    {
        private const string BaseUrl = "http://regrando.somee.com/regrandoo/api/Alimentos";
        private readonly HttpClient _httpClient;

        public AlimentoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Alimento>> BuscarAlimentos(string termo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}?termo={termo}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Alimento>>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar alimentos: {ex.Message}");
                throw;
            }
        }
    }
}
