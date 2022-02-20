using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using library_management_REST_API.DataAccess;
using library_management_REST_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]     //check auto ModelState
public class BooksController : ControllerBase
{
    private readonly myDbContext _context;
    private readonly IWebHostEnvironment _HostingEnv;
    public BooksController(myDbContext ctx, IWebHostEnvironment HostingEnv)
    {
        _context = ctx;
        _HostingEnv = HostingEnv;
    }

    [HttpGet]   //api/books
    public async Task<ActionResult<IEnumerable<Book>>> getBooksAsync()
    {
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
    public async Task<ActionResult> PostBookAsync([FromForm] Book book, IFormFile image)
    {  //ApiController check auto ModelState 
        book.imgUrl = uploadImg(image);
        var result = await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(getBookAsync), "Books", new { id = result.Entity.NoOfBooks }, result.Entity);/*returning a 201 Created response, with a Location header pointing to the url for the newly created response, and the object itself in the body. The url should be the url at which a GET request would return the object url. This would be considered the 'Correct' behaviour in a RESTful system.*/
    }

    [HttpPut("{id}")]    //api/books/1
    public async Task<ActionResult> PutBookAsync([FromRoute] int id, [FromForm] Book book, [FromForm] IFormFile image)
    {        //ApiController check auto ModelState 
        if (!_context.Books.Contains(book))
        {
            return NotFound($"Book with id={id} is not found");
        }
        if (id != book.NoOfBooks)
        {
            return BadRequest("Bad request");
        } 
        book.imgUrl = uploadImg(image);
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpDelete("{id}")]    //api/books/1
    public async Task<ActionResult> DeleteBookAsync(int id)
    {
        var bookToDelete = await _context.Books.FindAsync(id);
        if (bookToDelete == null)
        {
            return NotFound($"Book with id={id} is not found");
        }
        System.IO.File.Delete(bookToDelete.imgUrl);
        _context.Books.Remove(bookToDelete);
        await _context.SaveChangesAsync();
        return Ok(bookToDelete);

    }

    [HttpGet("search")] //api/books/search?title=des
    public async Task<ActionResult> SearchBookByTitleAsync(string title)
    {
        var searchResult = await _context.Books.Where(b => b.Title.Contains(title)).ToListAsync();
        if (searchResult.Any())
            return Ok(searchResult);
        return NotFound();
    }

    private string uploadImg(IFormFile img)
    {
        string UniqueFileName=  Guid.NewGuid().ToString().Substring(0,10)+"_"+img.FileName;  //if (!System.IO.File.Exists(ImgPath)) {   //ImgPath+="2";  }
        var ImgPath = Path.Combine(_HostingEnv.WebRootPath, "images",UniqueFileName);
        var ImgStream = new FileStream(ImgPath, FileMode.Append);
        img.CopyTo(ImgStream);
        return ImgPath;
    }
}