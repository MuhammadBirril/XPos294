using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ViewModel;
using WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebApi.Security;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ReadableBodyStream(Roles = "Administrator,Order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository orderRepo = new OrderRepository(ClaimsContext.UserName());

        [HttpPost]
        public async Task<ResponseResult> Post(OrderHeaderViewModel model)
        {
            return orderRepo.Create(model);
        }

    }
}
