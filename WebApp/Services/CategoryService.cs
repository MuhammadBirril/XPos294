using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Controllers;
using WebApp.Security;

namespace WebApp.Services
{
    public class CategoryService : IService<CategoryViewModel>
    {
        private readonly IConfiguration _configuration;
        private readonly string WebApiBaseUrl;
        private ResponseResult result = new ResponseResult();

        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<ResponseResult> Create(CategoryViewModel ent)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                    string strPayLoad = JsonConvert.SerializeObject(ent);
                    HttpContent content = new StringContent(strPayLoad, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(WebApiBaseUrl + "/Category", content))
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

        public async Task<ResponseResult> Delete(CategoryViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.DeleteAsync(WebApiBaseUrl + "/Category/" + ent.Id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                }
            }
            return result;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            using (var httpClient = new HttpClient())
            {
                //string token = ContextAccessor.GetToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + "/Category"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(apiResponse);
                }
            }
            return result;
        }

        public async Task<CategoryViewModel> GetById(long id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + $"/Category/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CategoryViewModel>(apiResponse);
                }
            }
            return result;
        }

        public async Task<ResponseResult> Update(CategoryViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                string strPayLoad = JsonConvert.SerializeObject(ent);
                HttpContent content = new StringContent(strPayLoad, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(WebApiBaseUrl + "/Category", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                }
                return result;
            }
        }
    }
}
