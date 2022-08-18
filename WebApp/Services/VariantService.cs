using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ViewModel;

namespace WebApp.Services
{
    public class VariantService : IService<VariantViewModel>
    {
        private readonly IConfiguration _configuration;
        private readonly string webApiBaseUrl;

        public VariantService(IConfiguration configuration)
        {
            _configuration = configuration;
            webApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<List<VariantViewModel>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<VariantViewModel> GetById(long id)
        {
            VariantViewModel result = new VariantViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(webApiBaseUrl + "/Variant/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<VariantViewModel>(apiResponse);
                }
            }
            return result;
        }
    }
}
