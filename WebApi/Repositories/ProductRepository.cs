using System;
using System.Collections.Generic;
using ViewModel;
using System.Linq;
using WebApi.DataModels;

namespace WebApi.Repositories
{
    public class ProductRepository : IRepositories<ProductViewModel>
    {
        private XposDbContext _XPosDbContext = new XposDbContext();
        private ResponseResult result = new ResponseResult();

        private string _UserName;

        public ProductRepository()
        {
            _UserName = "Birril";
        }

        public ProductRepository(string username)
        {
            _UserName = username;
        }

        public ResponseResult Create(ProductViewModel entity)
        {
            try
            {
                Product item = new Product();
                item.VariantId = entity.VariantId;
                item.Initial = entity.Initial;
                item.Name = entity.Name;
                item.Description = entity.Description;
                item.Price = entity.Price;
                item.Stock = entity.Stock;
                item.Active = entity.Active;

                item.CreateBy = _UserName;
                item.CreateDate = DateTime.Now;

                _XPosDbContext.Products.Add(item);
                _XPosDbContext.SaveChanges();

                result.Entity = item;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            return result;
        }

        public ResponseResult Delete(ProductViewModel entity)
        {
            try
            {
                Product item = _XPosDbContext.Products
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    result.Entity = item;
                    _XPosDbContext.Products.Remove(item);
                    _XPosDbContext.SaveChanges();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Not found";
                    result.Entity = entity;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            return result;
        }

        public List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            try
            {
                result = (from o in _XPosDbContext.Products
                          select new ProductViewModel
                          {
                              Id = o.Id,
                              VariantId = o.VariantId,
                              VariantName = o.Variant.Name,
                              Initial = o.Initial,
                              Name = o.Name,
                              Description = o.Description,
                              Price = o.Price,
                              Stock = o.Stock,
                              Active = o.Active
                          }).ToList();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }
        public ProductViewModel GetById(long id)
        {
            ProductViewModel result = new ProductViewModel();
            try
            {
                result = (from o in _XPosDbContext.Products
                          where o.Id == id
                          select new ProductViewModel
                          {
                              Id = o.Id,
                              VariantId = o.VariantId,
                              VariantName = o.Variant.Name,
                              Initial = o.Initial,
                              Name = o.Name,
                              Description = o.Description,
                              Price = o.Price,
                              Stock = o.Stock,
                              Active = o.Active
                          }).FirstOrDefault();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }
        public ResponseResult Update(ProductViewModel entity)
        {
            try
            {
                Product item = _XPosDbContext.Products
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    item.VariantId = entity.VariantId;
                    item.Initial = entity.Initial;
                    item.Name = entity.Name;
                    item.Description = entity.Description;
                    item.Price = entity.Price;
                    item.Stock = entity.Stock;
                    item.Active = entity.Active;

                    item.ModifyBy = _UserName;
                    item.ModifyDate = DateTime.Now;

                    _XPosDbContext.SaveChanges();

                    result.Entity = item;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Not found";
                    result.Entity = entity;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            return result;
        }
    }
}