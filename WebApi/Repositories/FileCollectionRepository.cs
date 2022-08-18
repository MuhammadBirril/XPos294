using System;
using System.Collections.Generic;
using ViewModel;
using System.Linq;
using WebApi.DataModels;

namespace WebApi.Repositories
{
    public class FileCollectionRepository : IRepositories<FileCollectionViewModel>
    {
        private XposDbContext _XPosDbContext = new XposDbContext();
        private ResponseResult result = new ResponseResult();
        private string _UserName;

        public FileCollectionRepository()
        {
            _UserName = "Birril";
        }

        public FileCollectionRepository(string username)
        {
            _UserName = username;
        }

        public ResponseResult Create(FileCollectionViewModel entity)
        {
            try
            {
                FileCollection item = new FileCollection();
                item.Title = entity.Title;
                item.FileName = entity.FileName;
                item.Active = entity.Active;

                item.CreateBy = _UserName;
                item.CreateDate = DateTime.Now;

                _XPosDbContext.FileCollections.Add(item);
                _XPosDbContext.SaveChanges();

                result.Entity = item;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public ResponseResult Delete(FileCollectionViewModel entity)
        {
            try
            {
                FileCollection item = _XPosDbContext.FileCollections
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    result.Entity = item;
                    _XPosDbContext.FileCollections.Remove(item);
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

        public List<FileCollectionViewModel> GetAll()
        {
            List<FileCollectionViewModel> result = new List<FileCollectionViewModel>();
            try
            {
                result = (from o in _XPosDbContext.FileCollections
                          select new FileCollectionViewModel
                          {
                              Id = o.Id,
                              Title = o.Title,
                              FileName = o.FileName,
                              Active = o.Active
                          }).ToList();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public FileCollectionViewModel GetById(long id)
        {
            FileCollectionViewModel result = new FileCollectionViewModel();
            try
            {
                result = (from o in _XPosDbContext.FileCollections
                          where o.Id == id
                          select new FileCollectionViewModel
                          {
                              Id = o.Id,
                              Title = o.Title,
                              FileName = o.FileName,
                              Active = o.Active
                          }).FirstOrDefault();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public ResponseResult Update(FileCollectionViewModel entity)
        {
            try
            {
                FileCollection item = _XPosDbContext.FileCollections
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    item.Title = entity.Title;
                    item.FileName = entity.FileName;
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
