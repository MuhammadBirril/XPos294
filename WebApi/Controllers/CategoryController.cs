using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;
using WebApi.Repositories;
using WebApi.Security;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ReadableBodyStream(Roles = "Administrator,Category")]
    public class CategoryController : ControllerBase
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
            //var result = categoryRepo.Delete(model);
            //if (!result)
            //    return new CategoryViewModel();
            //return model;
            CategoryViewModel model = new CategoryViewModel() { Id = id };
            return categoryRepo.Delete(model);
        }
    }
}
