using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Controllers;
using WebApp.Security;

namespace WebApp.Services
{
    public class VariantService : IService<VariantViewModel>
    {
        private readonly IConfiguration _configuration;
        private readonly string WebApiBaseUrl;
        private ResponseResult result = new ResponseResult();

        public VariantService(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<ResponseResult> Create(VariantViewModel ent)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                    string strPayload = JsonConvert.SerializeObject(ent);
                    HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(WebApiBaseUrl + "/Variant", content))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                result.Entity = ent;
            }
            return result;
        }

        public async Task<ResponseResult> Delete(VariantViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.DeleteAsync(WebApiBaseUrl + "/Variant/" + ent.Id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                }
            }
            return result;
        }

        public async Task<List<VariantViewModel>> GetAll()
        {
            List<VariantViewModel> result = new List<VariantViewModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + "/Variant"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<VariantViewModel>>(apiResponse);
                }
            }
            return result;
        }

        public async Task<VariantViewModel> GetById(long id)
        {
            VariantViewModel result = new VariantViewModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + "/Variant/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<VariantViewModel>(apiResponse);
                }
            }
            return result;
        }

        public async Task<ResponseResult> Update(VariantViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                string strPayload = JsonConvert.SerializeObject(ent);
                HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(WebApiBaseUrl + "/Variant", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                }
            }
            return result;
        }
    }
}
