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
    public class ProductService : IService<ProductViewModel>
    {
        private readonly IConfiguration _configuration;
        private readonly string WebApiBaseUrl;
        private ResponseResult result = new ResponseResult();

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<ResponseResult> Create(ProductViewModel ent)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                    string strPayload = JsonConvert.SerializeObject(ent);
                    HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(WebApiBaseUrl + "/Product", content))
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

        public async Task<ResponseResult> Delete(ProductViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.DeleteAsync(WebApiBaseUrl + "/Product/" + ent.Id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                }
            }
            return result;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + "/Product"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<ProductViewModel>>(apiResponse);
                }
            }
            return result;
        }

        public async Task<ProductViewModel> GetById(long id)
        {
            ProductViewModel result = new ProductViewModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + "/Product/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ProductViewModel>(apiResponse);
                }
            }
            return result;
        }

        public async Task<ResponseResult> Update(ProductViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                string strPayload = JsonConvert.SerializeObject(ent);
                HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(WebApiBaseUrl + "/Product", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                }
            }
            return result;
        }
    }
}
