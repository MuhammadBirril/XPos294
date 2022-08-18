using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace WebApp.Services
{
    public class AccountService
    {
        private readonly IConfiguration _configuration;
        private readonly string webApiBaseUrl;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
            webApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<AccountViewModel> Authentication(UserLogin login)
        {
            AccountViewModel result = new AccountViewModel();
            using (var httpClient = new HttpClient())
            {
                string strPayload = JsonConvert.SerializeObject(login);
                HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(webApiBaseUrl + "/account", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AccountViewModel>(apiResponse);
                }
            }
            return result;
        }
    }
}
