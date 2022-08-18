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
    [Authorize(Roles = "Administrator,Product")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly VariantService varServ;
        private readonly ProductService proServ;

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            varServ = new VariantService(configuration);
            proServ = new ProductService(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            List<ProductViewModel> list = await proServ.GetAll();
            return PartialView("_List", list);
        }

        public async Task<IActionResult> Create()
        {
            List<VariantViewModel> variantList = await varServ.GetAll();
            ViewBag.VariantList = new SelectList(variantList, "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await proServ.Create(model);
                if (result.Success)
                {
                    ViewBag.Title = "Create";
                    ViewBag.Body = "Created!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            List<VariantViewModel> variantList = await varServ.GetAll();
            ViewBag.VariantList = new SelectList(variantList, "Id", "Name");
            return PartialView("_Create", model);
        }

        public async Task<IActionResult> Edit(long id)
        {
            List<VariantViewModel> variantList = await varServ.GetAll();
            ViewBag.VariantList = new SelectList(variantList, "Id", "Name");
            ProductViewModel result = await proServ.GetById(id);
            return PartialView("_Edit", result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await proServ.Update(model);
                if (result.Success)
                {
                    ViewBag.Title = "Edit";
                    ViewBag.Body = "Changed!";
                    return PartialView("_Success", model);
                }
                else
                    ViewBag.ErrorMessage = result.Message;
            }
            List<VariantViewModel> variantList = await varServ.GetAll();
            ViewBag.VariantList = new SelectList(variantList, "Id", "Name");
            return PartialView("_Edit", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            ProductViewModel result = await proServ.GetById(id);
            return PartialView("_Details", result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            ProductViewModel result = await proServ.GetById(id);
            return PartialView("_Delete", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await proServ.Delete(model);
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

