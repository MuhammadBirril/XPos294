//using System;
//using System.Collections.Generic;
//using ViewModel;
//using System.Linq;
//using WebApi.DataModels;

//namespace WebApi.Repositories
//{
//    public class OrderDetailRepository : IRepositories<OrderDetailViewModel>
//    {
//        private XposDbContext _XPosDbContext = new XposDbContext();

//        private string _UserName;

//        public OrderDetailRepository()
//        {
//            _UserName = "Birril";
//        }

//        public OrderDetailRepository(string username)
//        {
//            _UserName = username;
//        }

//        public bool Create(OrderDetailViewModel entity)
//        {
//            bool result = true;
//            try
//            {
//                OrderDetail item = new OrderDetail();
//                item.HeaderId = entity.HeaderId;
//                item.ProductId = entity.ProductId;
//                item.Quantity = entity.Quantity;
//                item.Price = entity.Price;

//                item.CreateBy = _UserName;
//                item.CreateDate = DateTime.Now;


//                _XPosDbContext.OrderDetails.Add(item);
//                _XPosDbContext.SaveChanges();
//            }
//            catch (Exception e)
//            {
//                string error = e.Message;
//            }
//            return result;
//        }

//        public bool Delete(OrderDetailViewModel entity)
//        {
//            bool result = true;
//            try
//            {
//                OrderDetail item = _XPosDbContext.OrderDetails
//                            .Where(o => o.Id == entity.Id)
//                            .FirstOrDefault();
//                if (item != null)
//                {
//                    _XPosDbContext.OrderDetails.Remove(item);
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

//        public List<OrderDetailViewModel> GetAll()
//        {
//            List<OrderDetailViewModel> result = new List<OrderDetailViewModel>();
//            try
//            {
//                result = (from o in _XPosDbContext.OrderDetails
//                          select new OrderDetailViewModel
//                          {
//                              Id = o.Id,
//                              HeaderId = o.HeaderId,
//                              ProductId = o.ProductId,
//                              Quantity = o.Quantity,
//                              Price = o.Price
//                          }).ToList();
//            }
//            catch (Exception e)
//            {
//                string error = e.Message;
//            }
//            return result;
//        }

//        public OrderDetailViewModel GetById(long id)
//        {
//            OrderDetailViewModel result = new OrderDetailViewModel();
//            try
//            {
//                result = (from o in _XPosDbContext.OrderDetails
//                          where o.Id == id
//                          select new OrderDetailViewModel
//                          {
//                              Id = o.Id,
//                              HeaderId = o.HeaderId,
//                              ProductId = o.ProductId,
//                              Quantity = o.Quantity,
//                              Price = o.Price
//                          }).FirstOrDefault();
//            }
//            catch (Exception e)
//            {
//                string error = e.Message;
//            }
//            return result;
//        }

//        public bool Update(OrderDetailViewModel entity)
//        {
//            bool result = true;
//            try
//            {
//                OrderDetail item = _XPosDbContext.OrderDetails
//                            .Where(o => o.Id == entity.Id)
//                            .FirstOrDefault();
//                if (item != null)
//                {
//                    item.HeaderId = entity.HeaderId;
//                    item.ProductId = entity.ProductId;
//                    item.Quantity = entity.Quantity;
//                    item.Price = entity.Price;

//                    item.ModifyBy = _UserName;
//                    item.ModifyDate = DateTime.Now;

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
//    }
//}