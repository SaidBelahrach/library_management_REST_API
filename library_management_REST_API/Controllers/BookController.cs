using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_management_REST_API.DataAccess;
using library_management_REST_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
[ApiController]
[Route("api/[controller]")]     //check auto ModelState
public class BooksController : ControllerBase
{
    private readonly myDbContext _context;
    private readonly ILogger<BooksController> _logger;
    public BooksController(myDbContext ctx, ILogger<BooksController> logger)
    {
        _context = ctx;
        _logger = logger;
    }

    [HttpGet]   //api/books
    public async Task<ActionResult<IEnumerable<Book>>> getBooksAsync()
    {
        throw new UnauthorizedAccessException("errorrr");
        return Ok(await _context.Books.ToListAsync());
    }

    [HttpGet("{id:int}")]    //api/books/1
    public async Task<ActionResult> getBookAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.NoOfBooks == id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]   //api/books
    public async Task<ActionResult> PostBookAsync(Book book)
    {
        if (!ModelState.IsValid || book == null)
        {
            ModelState.AddModelError("invalid", "Model not valid");
            return BadRequest(ModelState);
        }
        var result = await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(getBookAsync),"Books", new { id = result.Entity.NoOfBooks }, result.Entity);/*returning a 201 Created response, with a Location header pointing to the url for the newly created response, and the object itself in the body. The url should be the url at which a GET request would return the object url. This would be considered the 'Correct' behaviour in a RESTful system.*/
    }

    [HttpPut("{id}")]    //api/books/1
    public async Task<ActionResult> PutBookAsync(int id, Book book)
    {        //check auto ModelState
        var bookToUpdate = await _context.Books.FirstOrDefaultAsync(b => b.NoOfBooks == id);
        if (bookToUpdate == null)
        {
            return NotFound($"Book with id={id} is not found");
        }
        if (id == book.NoOfBooks)
        {
            return BadRequest("Bad request");
        }
        bookToUpdate.prix = book.prix;
        bookToUpdate.Title = book.Title;
        bookToUpdate.Author = book.Author;
        await _context.SaveChangesAsync();
        return Ok(bookToUpdate);
    }

    [HttpDelete("{id}")]    //api/books/1
    public async Task<ActionResult> DeleteBookAsync(int id)
    {
        var bookToDelete = await _context.Books.FindAsync(id);
        if (bookToDelete == null)
        {
            return NotFound($"Book with id={id} is not found");
        }
        _context.Books.Remove(bookToDelete);
        await _context.SaveChangesAsync();
        return NoContent();

    }

    [HttpGet("search")] //api/books/search?title=des
    public async Task<ActionResult> SearchBookByTitleAsync(string title)
    {
        var searchResult = await _context.Books.Where(b => b.Title.Contains(title)).ToListAsync();
        if(searchResult.Any())
            return Ok(searchResult);
        return NotFound();
    }
}