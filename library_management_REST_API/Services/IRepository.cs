using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_management_REST_API.Models;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(T id);
    List<T> GetBy(Func<T, bool> where);
    Task<T> AddAsync(T t);
    Task<bool> UpdateAsync(T t);
    Task<bool> DeleteAsync(T id);
}