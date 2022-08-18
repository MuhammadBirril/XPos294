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
    public class BookService : IService<BookViewModel>
    {
        private readonly IConfiguration _configuration;
        private readonly string WebApiBaseUrl;
        private ResponseResult result = new ResponseResult();

        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiBaseUrl = _configuration.GetValue<string>("WebApiBaseUrl");
        }

        public async Task<ResponseResult> Create(BookViewModel ent)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                    string strPayLoad = JsonConvert.SerializeObject(ent);
                    HttpContent content = new StringContent(strPayLoad, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(WebApiBaseUrl + "/Book", content))
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

        public async Task<ResponseResult> Delete(BookViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.DeleteAsync(WebApiBaseUrl + "/Book/" + ent.Id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                }
            }
            return result;
        }

        public async Task<List<BookViewModel>> GetAll()
        {
            List<BookViewModel> result = new List<BookViewModel>();
            using (var httpClient = new HttpClient())
            {
                //string token = ContextAccessor.GetToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + "/Book"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<BookViewModel>>(apiResponse);
                }
            }
            return result;
        }

        public async Task<BookViewModel> GetById(long id)
        {
            BookViewModel result = new BookViewModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                using (var response = await httpClient.GetAsync(WebApiBaseUrl + $"/Book/{id}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<BookViewModel>(apiResponse);
                }
            }
            return result;
        }

        public async Task<ResponseResult> Update(BookViewModel ent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ContextAccessor.GetToken());
                string strPayLoad = JsonConvert.SerializeObject(ent);
                HttpContent content = new StringContent(strPayLoad, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(WebApiBaseUrl + "/Book", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                }
                return result;
            }
        }
    }
}
