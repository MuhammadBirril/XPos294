using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using WebApi.DataModels;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class BukuRepository : IRepositories<BookViewModel>
    {
        private XposDbContext _XposDbContext = new XposDbContext();
        private ResponseResult result = new ResponseResult();
        private string _Username;
        public BukuRepository()
        {
            _Username = "Birril";
        }
        public BukuRepository(string userName)
        {
            _Username = userName;
        }

        public ResponseResult Create(BookViewModel entity)
        {
            try
            {
                Book item = new Book();
                item.KodeBuku = entity.KodeBuku;
                item.NamaBuku = entity.NamaBuku;
                item.Active = entity.Active;

                item.CreateBy = _Username;
                item.CreateDate = DateTime.Now;

                _XposDbContext.Books.Add(item);
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

        public ResponseResult Delete(BookViewModel entity)
        {
            try
            {
                Book item = _XposDbContext.Books.Where(o => o.Id == entity.Id).FirstOrDefault();
                if (item != null)
                {
                    result.Entity = item;
                    _XposDbContext.Books.Remove(item);
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

        public List<BookViewModel> GetAll()
        {
            List<BookViewModel> result = new List<BookViewModel>();
            try
            {
                result = (from o in _XposDbContext.Books
                          select new BookViewModel
                          {
                              Id = o.Id,
                              KodeBuku = o.KodeBuku,
                              NamaBuku = o.NamaBuku,
                              Active = o.Active
                          }).ToList();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public BookViewModel GetById(long id)
        {
            BookViewModel result = new BookViewModel();
            try
            {
                result = (from o in _XposDbContext.Books
                          where o.Id == id
                          select new BookViewModel
                          {
                              Id = o.Id,
                              KodeBuku = o.KodeBuku,
                              NamaBuku = o.NamaBuku,
                              Active = o.Active
                          }).FirstOrDefault();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public ResponseResult Update(BookViewModel entity)
        {
            try
            {
                Book item = _XposDbContext.Books.Where(o => o.Id == entity.Id).FirstOrDefault();
                if (item != null)
                {
                    item.KodeBuku = entity.KodeBuku;
                    item.NamaBuku = entity.NamaBuku;
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
