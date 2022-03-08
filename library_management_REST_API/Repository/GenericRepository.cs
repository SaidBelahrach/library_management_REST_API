using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_management_REST_API.Models;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> where T:class
{
    private readonly myDbContext _context;
    private readonly DbSet<T> dbSet;

    public GenericRepository(myDbContext context)
    {
        this._context = context;
        this.dbSet = context.Set<T>();
    }

    public async Task<List<T>> Get()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T> GetByID(object id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task Insert(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual void Delete(object id)
    {
        T entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(T entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(T entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
    public void Save()
    {
        _context.SaveChanges();
    }
} 