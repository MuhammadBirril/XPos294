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
    [ReadableBodyStream(Roles = "Administrator,Variant")]
    public class VariantController : ControllerBase
    {
        private VariantRepository variantRepo = new VariantRepository(ClaimsContext.UserName());

        [HttpGet]
        public async Task<List<VariantViewModel>> Get()
        {
            return variantRepo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<VariantViewModel> Get(long id)
        {
            return variantRepo.GetById(id);
        }

        [HttpPost]
        public async Task<ResponseResult> Post(VariantViewModel model)
        {
            //var result = variantRepo.Create(model);
            //if (!result)
            //    return new VariantViewModel();
            //return model;
            return variantRepo.Create(model);
        }

        [HttpPut]
        public async Task<ResponseResult> Put(VariantViewModel model)
        {
            //var result = variantRepo.Update(model);
            //if (!result)
            //    return new VariantViewModel();
            //return model;
            return variantRepo.Update(model);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseResult> Delete(long id)
        {
            //var result = variantRepo.Delete(model);
            //if (!result)
            //    return new VariantViewModel();
            //return model;
            VariantViewModel model = new VariantViewModel() { Id = id };
            return variantRepo.Delete(model);
        }
    }
}
