using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator,Category")]
    public class CategoryController : Controller
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string webApiBaseUrl;
        private readonly CategoryService catServ;

        public CategoryController(ILogger<CategoryController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            webApiBaseUrl = _configuration.GetValue<string>("webApiBaseUrl");
            catServ = new CategoryService(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            List<CategoryViewModel> list = await catServ.GetAll();
            //List<CategoryViewModel> list = new List<CategoryViewModel>();
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync(webApiBaseUrl + "/category"))
            //    {
            //        var apiResponse = await response.Content.ReadAsStringAsync();
            //        list = JsonConvert.DeserializeObject<List<CategoryViewModel>>(apiResponse);
            //    }
            //}
            //return PartialView("_List", list);
            return PartialView("_List", list);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = new ResponseResult();
                using (var httpClient = new HttpClient())
                {
                    string strPayload = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(webApiBaseUrl + "/category", content))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                        if (result.Success)
                        {
                            ViewBag.Message = "Created Successfully";
                            ViewBag.Status = "Created";
                            return PartialView("_Success", model);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = result.Message;
                        }
                    }
                }
            }
            return PartialView("_Create", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(webApiBaseUrl + "/category/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CategoryViewModel>(apiResponse);
                    if (result != null)
                    {
                        return PartialView("_Edit", result);
                    }
                }
            }
            return PartialView("_Edit", result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = new ResponseResult();
                using (var httpClient = new HttpClient())
                {
                    string strPayload = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(webApiBaseUrl + "/category", content))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                        if (result.Success)
                        {
                            ViewBag.Message = "Edited Successfully";
                            ViewBag.Status = "Edited";
                            return PartialView("_Success", model);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = result.Message;
                        }
                    }
                }
            }
            return PartialView("_Edit", model);
        }
        public async Task<IActionResult> Details(int id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(webApiBaseUrl + "/category/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CategoryViewModel>(apiResponse);
                    if (result != null)
                    {
                        return PartialView("_Details", result);
                    }
                }
            }
            return PartialView("_Details", result);
        }
        public async Task<IActionResult> Delete(int id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(webApiBaseUrl + "/Category/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CategoryViewModel>(apiResponse);
                    if (result != null)
                        return PartialView("_Delete", result);
                }
            }
            return PartialView("_Delete", result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = new ResponseResult();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(webApiBaseUrl + "/Category/" + model.Id))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);
                        if (result.Success)
                        {
                            ViewBag.Message = "Deleted Successfully";
                            ViewBag.Status = "Deleted";
                            return PartialView("_Success", model);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = result.Message;
                        }
                    }
                }
            }
            return PartialView("_Delete", model);
        }
    }
}
