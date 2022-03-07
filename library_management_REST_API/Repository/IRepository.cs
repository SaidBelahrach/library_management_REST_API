using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(T id);
    List<T> GetBy(Func<T, bool> where);
    Task<T> AddAsync(T t);
    Task<bool> UpdateAsync(T t);
    Task<bool> DeleteAsync(T id);
}