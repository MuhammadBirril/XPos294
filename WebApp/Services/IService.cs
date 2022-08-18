using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IService<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(long id);
    }
}
