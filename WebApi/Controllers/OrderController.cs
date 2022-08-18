//using System;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using WebApi.Models;
//using System.Collections.Generic;
//using System.Linq;
//using ViewModel;
//using WebApi.Repositories;

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderController : ControllerBase
//    {
//        private readonly OrderRepository orderHeaderRepo = new OrderRepository();

//        [HttpGet]
//        public async Task<List<OrderHeaderViewModel>> Get()
//        {
//            return orderHeaderRepo.GetAll();
//        }

//        [HttpGet("{id}")]
//        public async Task<OrderHeaderViewModel> Get(long id)
//        {
//            return orderHeaderRepo.GetById(id);
//        }

//        [HttpPost]
//        public async Task<OrderHeaderViewModel> Post(OrderHeaderViewModel model)
//        {
//            if (!orderHeaderRepo.Create(model))
//                return new OrderHeaderViewModel();
//            return model;
//        }

//        [HttpPut]
//        public async Task<OrderHeaderViewModel> Put(OrderHeaderViewModel model)
//        {
//            var result = orderHeaderRepo.Update(model);
//            if (!result)
//                return new OrderHeaderViewModel();
//            return model;
//        }

//        [HttpDelete]
//        public async Task<OrderHeaderViewModel> Delete(OrderHeaderViewModel model)
//        {
//            var result = orderHeaderRepo.Delete(model);
//            if (!result)
//                return new OrderHeaderViewModel();
//            return model;
//        }
//    }
//}
