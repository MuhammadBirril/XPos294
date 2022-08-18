using System;
using System.Collections.Generic;
using ViewModel;
using System.Linq;
using WebApi.DataModels;

namespace WebApi.Repositories
{
    public class VariantRepository : IRepositories<VariantViewModel>
    {
        private XposDbContext _XPosDbContext = new XposDbContext();
        private ResponseResult result = new ResponseResult();

        private string _UserName;

        public VariantRepository()
        {
            _UserName = "Birril";
        }

        public VariantRepository(string username)
        {
            _UserName = username;
        }

        public ResponseResult Create(VariantViewModel entity)
        {
            try
            {
                Variant item = new Variant();
                item.CategoryId = entity.CategoryId;
                item.Initial = entity.Initial;
                item.Name = entity.Name;
                item.Active = entity.Active;

                item.CreateBy = _UserName;
                item.CreateDate = DateTime.Now;

                _XPosDbContext.Variants.Add(item);
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

        public ResponseResult Delete(VariantViewModel entity)
        {
            try
            {
                Variant item = _XPosDbContext.Variants
                    .Where(o => o.Id == entity.Id)
                    .FirstOrDefault();
                if (item != null)
                {
                    result.Entity = item;
                    _XPosDbContext.Variants.Remove(item);
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

        public List<VariantViewModel> GetAll()
        {
            List<VariantViewModel> result = new List<VariantViewModel>();
            try
            {
                result = (from o in _XPosDbContext.Variants
                          select new VariantViewModel
                          {
                              Id = o.Id,
                              CategoryId = o.CategoryId,
                              CategoryName = o.Category.Name,
                              Initial = o.Initial,
                              Name = o.Name,
                              Active = o.Active
                          }).ToList();

            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public VariantViewModel GetById(long id)
        {
            VariantViewModel result = new VariantViewModel();
            try
            {
                result = (from o in _XPosDbContext.Variants
                          where o.Id == id
                          select new VariantViewModel
                          {
                              Id = o.Id,
                              CategoryId = o.CategoryId,
                              CategoryName = o.Category.Name,
                              Initial = o.Initial,
                              Name = o.Name,
                              Active = o.Active
                          }).FirstOrDefault();

            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public ResponseResult Update(VariantViewModel entity)
        {
            try
            {
                Variant item = _XPosDbContext.Variants
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    item.Initial = entity.Initial;
                    item.Name = entity.Name;
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
                result.Success = false;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
