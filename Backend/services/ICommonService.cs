using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.services
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI newObject);
        Task<T> Update(int id, TU changedObject);
        Task<T> Delete(int id);
    }
}
