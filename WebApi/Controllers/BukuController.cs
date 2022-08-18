using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.Repositories;
using ViewModel;
using WebApi.Security;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ReadableBodyStream(Roles = "Administrator,Buku")]

    public class BukuController : ControllerBase
    {
        private CategoryRepository categoryRepo = new CategoryRepository(ClaimsContext.UserName());
        [HttpGet]
        public async Task<List<CategoryViewModel>> Get()
        {
            return categoryRepo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> Get(long id)
        {
            return categoryRepo.GetById(id);
        }

        [HttpPost]
        public async Task<ResponseResult> Post(CategoryViewModel model)
        {
            //var result = categoryRepo.Create(model);
            //if (!result)
            //    return new CategoryViewModel();
            //return model;
            return categoryRepo.Create(model);
        }

        [HttpPut]
        public async Task<ResponseResult> Put(CategoryViewModel model)
        {
            //var result = categoryRepo.Update(model);
            //if (!result)
            //    return new CategoryViewModel();
            //return model;
            return categoryRepo.Update(model);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseResult> Delete(long id)
        {
            CategoryViewModel model = new CategoryViewModel() { Id = id };
            return categoryRepo.Delete(model);
        }
    }
}
