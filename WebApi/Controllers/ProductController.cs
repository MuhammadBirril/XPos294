using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ViewModel;
using WebApi.Repositories;
using WebApi.Security;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ReadableBodyStream(Roles = "Administrator,Product")]
    public class ProductController : ControllerBase
    {
        private ProductRepository productRepo = new ProductRepository(ClaimsContext.UserName());

        [HttpGet]
        public async Task<List<ProductViewModel>> Get()
        {
            return productRepo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(long id)
        {
            return productRepo.GetById(id);
        }

        [HttpPost]
        public async Task<ResponseResult> Post(ProductViewModel model)
        {
            //var result = productRepo.Create(model);
            //if (!result)
            //    return new ProductViewModel();
            //return model;
            return productRepo.Create(model);
        }

        [HttpPut]
        public async Task<ResponseResult> Put(ProductViewModel model)
        {
            //var result = productRepo.Update(model);
            //if (!result)
            //    return new ProductViewModel();
            //return model;
            return productRepo.Update(model);
        }

        [HttpDelete]
        public async Task<ResponseResult> Delete(ProductViewModel model)
        {
            //var result = productRepo.Delete(model);
            //if (!result)
            //    return new ProductViewModel();
            //return model;
            return productRepo.Delete(model);
        }
    }
}
