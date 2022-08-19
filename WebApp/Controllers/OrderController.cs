using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Administrator,Order")]
    public class OrderController : Controller
    {

        private readonly ProductService proSrv;
        public OrderController (IConfiguration configuration)
        {
            proSrv = new ProductService (configuration);
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductList()
        {
            List<ProductViewModel> list = await proSrv.GetAll();
            return PartialView("_ProductList");
        }
    }
}
