using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;

namespace WebApi.Repositories
{
    public interface IRepositories<T>
    {
        List<T> GetAll();
        T GetById(long id);
        ResponseResult Create(T entity);
        ResponseResult Update(T entity);
        ResponseResult Delete(T entity);

    }
}
