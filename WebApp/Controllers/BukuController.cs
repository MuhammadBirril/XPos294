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
    [Authorize(Roles = "Administrator,Book")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly BookService bookServ;

        public BookController(ILogger<BookController> logger, IConfiguration configuration)
        {
            _logger = logger;
            bookServ = new BookService(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            List<BookViewModel> list = await bookServ.GetAll();
            return PartialView("_List", list);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await bookServ.Create(model);
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
            BookViewModel result = await bookServ.GetById(id);
            return PartialView("_Edit", result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await bookServ.Update(model);
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
            BookViewModel result = await bookServ.GetById(id);
            return PartialView("_Details", result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            BookViewModel result = await bookServ.GetById(id);
            return PartialView("_Delete", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseResult result = await bookServ.Delete(model);
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

