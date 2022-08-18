using System;
using System.Collections.Generic;
using ViewModel;
using System.Linq;
using WebApi.DataModels;

namespace WebApi.Repositories
{
    public class OrderRepository : IRepositories<OrderHeaderViewModel>
    {
        private XposDbContext _XposDbContext = new XposDbContext();
        private ResponseResult result = new ResponseResult();

        private string _UserName;

        public OrderRepository()
        {
            _UserName = "Birril";
        }

        public OrderRepository(string username)
        {
            _UserName = username;
        }

        public ResponseResult Create(OrderHeaderViewModel entity)
        {
            try
            {
                string newRef = NewReference();
                if (newRef != "")
                {
                    entity.Reference = newRef;

                    using var transaction = _XposDbContext.Database.BeginTransaction();

                    OrderHeader oh = new OrderHeader();
                    oh.Reference = entity.Reference;
                    oh.Amount = entity.Amount;

                    oh.CreateBy = _UserName;
                    oh.CreateDate = DateTime.Now;

                    _XposDbContext.OrderHeaders.Add(oh);
                    _XposDbContext.SaveChanges();

                    foreach (var item in entity.Details)
                    {
                        OrderDetail od = new OrderDetail();
                        od.HeaderId = oh.Id;
                        od.ProductId = item.ProductId;
                        od.Price = item.Price;
                        od.Quantity = item.Quantity;

                        od.CreateBy = _UserName;
                        od.CreateDate = DateTime.Now;

                        _XposDbContext.OrderDetails.Add(od);
                    }

                    _XposDbContext.SaveChanges();

                    transaction.Commit();
                    result.Entity = entity;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Not found";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }
            return result;
        }

        public ResponseResult Delete(OrderHeaderViewModel entity)
        {
            try
            {
                OrderHeader item = _XposDbContext.OrderHeaders
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    result.Entity = item;
                    _XposDbContext.OrderHeaders.Remove(item);
                    _XposDbContext.SaveChanges();
                }
                else
                {
                    result.Success = false;
                    result.Message = "Not Found!";
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

        public List<OrderHeaderViewModel> GetAll()
        {
            List<OrderHeaderViewModel> result = new List<OrderHeaderViewModel>();
            try
            {
                result = (from o in _XposDbContext.OrderHeaders
                          select new OrderHeaderViewModel
                          {
                              Id = o.Id,
                              Reference = o.Reference,
                              Amount = o.Amount
                          }).ToList();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public OrderHeaderViewModel GetById(long id)
        {
            OrderHeaderViewModel result = new OrderHeaderViewModel();
            try
            {
                result = (from o in _XposDbContext.OrderHeaders
                          where o.Id == id
                          select new OrderHeaderViewModel
                          {
                              Id = o.Id,
                              Reference = o.Reference,
                              Amount = o.Amount
                          }).FirstOrDefault();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return result;
        }

        public ResponseResult Update(OrderHeaderViewModel entity)
        {
            try
            {
                OrderHeader item = _XposDbContext.OrderHeaders
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();
                if (item != null)
                {
                    item.Reference = entity.Reference;
                    item.Amount = entity.Amount;

                    item.ModifyBy = _UserName;
                    item.ModifyDate = DateTime.Now;

                    _XposDbContext.SaveChanges();
                    result.Entity = item;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Not Found!";
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

        private string NewReference()
        {
            string yearMonth = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("D2");
            //2208
            string newRef = "SLS-" + yearMonth + "-";
            // SLS-2208-
            try
            {
                var maxRef = _XposDbContext.OrderHeaders
                        .Where(o => o.Reference.Contains(newRef))
                        .OrderByDescending(o => o.Reference)
                        .FirstOrDefault();

                if (maxRef != null)
                {
                    // SLS-2208-0230
                    string[] oldRef = maxRef.Reference.Split('-');
                    // ["SLS","2208","0230"]
                    int newInt = int.Parse(oldRef[2]) + 1;
                    // 231
                    newRef += newInt.ToString("D4");
                    // SLS-2208-0231
                }
                else
                {
                    newRef += "0001";
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                newRef = "";
            }
            // SLS-2208-0231
            return newRef;
        }
    }
}

