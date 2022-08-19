using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Security;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator,Category")]
    public class CategoryController : Controller
    {


        private readonly ILogger<CategoryController> _logger;

        private readonly CategoryService catServ;

        public CategoryController(ILogger<CategoryController> logger, IConfiguration configuration)
        {
            _logger = logger;
            //_configuration = configuration;
            //XPosWebApiBaseUrl = _configuration.GetValue<string>("XPosWebApiBaseUrl");
            catServ = new CategoryService(configuration);
            //new ContextAccessor(accessor);
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
            //    using (var response = await httpClient.GetAsync(XPosWebApiBaseUrl + "/Category"))
            //    {
            //        var apiResponse = await response.Content.ReadAsStringAsync();
            //        list = JsonConvert.DeserializeObject<List<CategoryViewModel>>(apiResponse);
            //    }
            //}
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
                ResponseResult result = await catServ.Create(model);
                if (result.Success)
                {
                    ViewBag.Title = "Create";
                    ViewBag.Body = "Created!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            return PartialView("_Create", model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CategoryViewModel result = await catServ.GetById(id);
            return PartialView("_Edit", result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await catServ.Update(model);
                if (result.Success)
                {
                    ViewBag.Title = "Edit";
                    ViewBag.Body = "Changed!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            return PartialView("_Edit", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            CategoryViewModel result = await catServ.GetById(id);
            return PartialView("_Details", result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            CategoryViewModel result = await catServ.GetById(id);
            return PartialView("_Delete", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await catServ.Delete(model);
                if (result.Success)
                {
                    ViewBag.Title = "Delete";
                    ViewBag.Body = "Deleted!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            return PartialView("_Delete", model);
        }
    }
}
