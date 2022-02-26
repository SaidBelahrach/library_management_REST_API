using library_management_REST_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
public class BookRepository
{
    private readonly myDbContext _context;
    private readonly IWebHostEnvironment _HostingEnv;

    public BookRepository(myDbContext context, IWebHostEnvironment HostingEnv)
    {
        _context = context;
        _HostingEnv = HostingEnv;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> FindBookByIdAsync(int bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.NoOfBooks == bookId);
    }
    public async Task<List<Book>> Get(Expression<Func<Book, bool>> exp)
    {
        return await _context.Books.Where(exp).ToListAsync();
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        var result = await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteBookAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Exists(int bookId)
    {
        return await _context.Books.AnyAsync(b => b.NoOfBooks == bookId);
    }
}