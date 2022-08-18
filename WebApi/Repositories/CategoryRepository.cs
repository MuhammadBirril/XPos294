using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using WebApi.DataModels;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class CategoryRepository : IRepositories<CategoryViewModel>
    {
        private XposDbContext _XposDbContext = new XposDbContext();
        private ResponseResult result = new ResponseResult();
        private string _Username;
        public CategoryRepository()
        {
            _Username = "Birril";
        }
        public CategoryRepository(string userName)
        {
            _Username = userName;
        }
        public ResponseResult Create(CategoryViewModel entity)
        {
            try
            {
                Category item = new Category();
                item.Initial = entity.Initial;
                item.Name = entity.Name;
                item.Active = entity.Active;

                item.CreateBy = _Username;
                item.CreateDate = DateTime.Now;

                _XposDbContext.Categories.Add(item);
                _XposDbContext.SaveChanges();

                result.Entity = item;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;

            }
            return result;
        }

        public ResponseResult Delete(CategoryViewModel entity)
        {
            try
            {
                Category item = _XposDbContext.Categories.Where(o => o.Id == entity.Id).FirstOrDefault();
                if(item!= null)
                {
                    result.Entity = item;
                    _XposDbContext.Categories.Remove(item);
                    _XposDbContext.SaveChanges();
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

        public List<CategoryViewModel> GetAll()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel> ();
            try
            {
                result = (from o in _XposDbContext.Categories
                          select new CategoryViewModel
                          {
                              Id = o.Id,
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

        public CategoryViewModel GetById(long id)
        {
            CategoryViewModel result = new CategoryViewModel();
            try
            {
                result = (from o in _XposDbContext.Categories
                          where o.Id == id
                          select new CategoryViewModel
                          {
                              Id = o.Id,
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

        public ResponseResult Update(CategoryViewModel entity)
        {
            try
            {
                Category item = _XposDbContext.Categories.Where(o => o.Id == entity.Id).FirstOrDefault();
                if(item != null)
                {
                    item.Initial = entity.Initial;
                    item.Name = entity.Name;
                    item.Active = entity.Active;

                    item.ModifyBy = _Username;
                    item.ModifyDate = DateTime.Now;

                    _XposDbContext.SaveChanges();
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