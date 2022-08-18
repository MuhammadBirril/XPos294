using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ViewModel;

namespace WebApp.Services
{
    public class CategoryService : IService<CategoryViewModel>
    {
        private readonly IConfiguration _configuration;
        private readonly string webApiBaseUrl;

        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
            webApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(webApiBaseUrl + "/category"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(apiResponse);
                }
            }
            return result;
        }

        public async Task<CategoryViewModel> GetById(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
