using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_management_REST_API.DataAccess;
using library_management_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class BookController:ControllerBase
{
    private readonly myDbContext _context;  
    private readonly ILogger<BookController> _logger;
    public BookController(myDbContext ctx, ILogger<BookController> logger) {
        _context=ctx;
        _logger=logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> getBooks(){
        return Ok( await _context.Books.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> getBook(int id){
        var book=await _context.Books.FirstOrDefaultAsync(b=>b.NoOfBooks==id);
        if(book==null){
            return NotFound();
        } 
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult> addBook(Book book){
        if(!ModelState.IsValid || book==null){
            ModelState.AddModelError("invalid","Model not valid");
            return BadRequest(ModelState);
        }  
        var result=await _context.Books.AddAsync(book);
        return Created(nameof(addBook),new {id=result.Entity.NoOfBooks});
    }
}
/*    var book=new Book();
        book.Author="said";
        book.prix=(decimal) 999;
        book.Title="GoF"; 
        try
        {
            _context.Books.Add(book);
         //   _context.SaveChanges(); 
        }catch(Exception e){
            return Content("internal error 500"+e.Message+" \n"+e);
        }*/