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
//    public class UserRoleController : ControllerBase
//    {
//        private UserRoleRepository userRoleRepo = new UserRoleRepository();

//        [HttpGet]
//        public async Task<List<UserRoleViewModel>> Get()
//        {
//            return userRoleRepo.GetAll();
//        }

//        [HttpGet("{id}")]
//        public async Task<UserRoleViewModel> Get(long id)
//        {
//            return userRoleRepo.GetById(id);
//        }

//        [HttpPost]
//        public async Task<UserRoleViewModel> Post(UserRoleViewModel model)
//        {
//            var result = userRoleRepo.Create(model);
//            if (!result)
//                return new UserRoleViewModel();
//            return model;
//        }

//        [HttpPut]
//        public async Task<UserRoleViewModel> Put(UserRoleViewModel model)
//        {
//            var result = userRoleRepo.Update(model);
//            if (!result)
//                return new UserRoleViewModel();
//            return model;
//        }

//        [HttpDelete]
//        public async Task<UserRoleViewModel> Delete(UserRoleViewModel model)
//        {
//            var result = userRoleRepo.Delete(model);
//            if (!result)
//                return new UserRoleViewModel();
//            return model;
//        }
//    }
//}
