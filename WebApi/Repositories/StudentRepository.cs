//using System;
//using System.Collections.Generic;
//using ViewModel;
//using System.Linq;
//using WebApi.DataModels;
//using WebApi.Models;

//namespace WebApi.Repositories
//{
//    public class StudentRepository : IRepositories<StudentViewModel>
//    {
//        private readonly XposDbContext _XPosDbContext = new XposDbContext();

//        private string _UserName;

//        public StudentRepository()
//        {
//            _UserName = "Birril";
//        }

//        public StudentRepository(string username)
//        {
//            _UserName = username;
//        }

//        public bool Create(StudentViewModel entity)
//        {
//            bool result = true;
//            try
//            {
//                Student item = new Student();
//                item.FirstName = entity.FirstName;
//                item.LastName = entity.LastName;
//                item.BirtDate = entity.BirtDate;
//                item.Gender = entity.Gender;

//                _XPosDbContext.Students.Add(item);
//                _XPosDbContext.SaveChanges();
//            }
//            catch (Exception e)
//            {
//                string error = e.Message;
//            }
//            return result;
//        }

//        public bool Delete(StudentViewModel entity)
//        {
//            bool result = true;
//            try
//            {
//                Student item = _XPosDbContext.Students
//                           .Where(x => x.Id == entity.Id)
//                           .FirstOrDefault();
//                if (item != null)
//                {
//                    _XPosDbContext.Students.Remove(item);
//                    _XPosDbContext.SaveChanges();
//                }
//                else
//                {
//                    result = false;
//                }
//            }
//            catch (Exception e)
//            {
//                string error = e.Message;
//            }
//            return result;
//        }

//        public List<StudentViewModel> GetAll()
//        {
//            List<StudentViewModel> result = new List<StudentViewModel>();
//            try
//            {
//                result = (from o in _XPosDbContext.Students
//                          select new StudentViewModel
//                          {
//                              Id = o.Id,
//                              FirstName = o.FirstName,
//                              LastName = o.LastName,
//                              BirtDate = o.BirtDate,
//                              Gender = o.Gender
//                          }).ToList();
//            }
//            catch (Exception e)
//            {
//                string error = e.Message;
//            }
//            return result;
//        }

//        public StudentViewModel GetById(long id)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Update(StudentViewModel entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
