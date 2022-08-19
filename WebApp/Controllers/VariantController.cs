using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    [Authorize(Roles = "Administrator,Variant")]
    public class VariantController : Controller
    {
        private readonly ILogger<VariantController> _logger;

        private readonly CategoryService catServ;
        private readonly VariantService varServ;

        public VariantController(ILogger<VariantController> logger, IConfiguration configuration)
        {
            _logger = logger;
            catServ = new CategoryService(configuration);
            varServ = new VariantService(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            List<VariantViewModel> list = await varServ.GetAll();
            return PartialView("_List", list);
        }

        public async Task<IActionResult> Create()
        {
            List<CategoryViewModel> categoryList = await catServ.GetAll();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(VariantViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await varServ.Create(model);
                if (result.Success)
                {
                    ViewBag.Title = "Create";
                    ViewBag.Body = "Created!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            List<CategoryViewModel> categoryList = await catServ.GetAll();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "Name");
            return PartialView("_Create", model);
        }

        public async Task<IActionResult> Edit(long id)
        {
            List<CategoryViewModel> categoryList = await catServ.GetAll();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "Name");
            VariantViewModel result = await varServ.GetById(id);
            return PartialView("_Edit", result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VariantViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await varServ.Update(model);
                if (result.Success)
                {
                    ViewBag.Title = "Edit";
                    ViewBag.Body = "Changed!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            List<CategoryViewModel> categoryList = await catServ.GetAll();
            ViewBag.CategoryList = new SelectList(categoryList, "Id", "Name");
            return PartialView("_Edit", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            VariantViewModel result = await varServ.GetById(id);
            return PartialView("_Details", result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            VariantViewModel result = await varServ.GetById(id);
            return PartialView("_Delete", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VariantViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await varServ.Delete(model);
                if (result.Success)
                {
                    ViewBag.Title = "Delete";
                    ViewBag.Body = "Deleted";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            return PartialView("_Delete", model);
        }
    }
}
