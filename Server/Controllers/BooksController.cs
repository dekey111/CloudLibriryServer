using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Server.DataBase;
using Server.Models;
using System.Runtime.CompilerServices;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        readonly CloudLibriryContext context;
        public BooksController(CloudLibriryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                List<BooksResponse> booksResponse = new List<BooksResponse>();
                context.Books.ToList().ForEach(x => booksResponse.Add(new BooksResponse(x)));
                return Ok(booksResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBook(int id)
        {
            try
            {
                var book = await context.Books.FirstOrDefaultAsync(x => x.IdBooks == id);
                if (book == null)
                    return BadRequest("Книга не найдена!");

                return Ok(new BooksResponse(book));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingBook([FromBody] BooksRequest bookReq)
        {
            try
            {
                var findBook = await context.Books.FirstOrDefaultAsync(x => x.Isbn == bookReq.Isbn);
                if (findBook != null)
                    return BadRequest("Книга с таким ISBN уже есть!");

                Book newBook = new Book()
                {
                    Isbn = bookReq.Isbn,
                    Name = bookReq.Name,
                    IdAutor = bookReq.IdAutor,
                    IdPublisher = bookReq.IdPublisher,
                    DatePublished = bookReq.DatePublished,
                    IdTypeCover = bookReq.IdTypeCover,
                    Language = bookReq.Language,
                    CountPages = bookReq.CountPages,
                    Desctiprtion = bookReq.Desctiprtion,
                    Cost = bookReq.Cost
                };
                context.Books.Add(newBook);
                await context.SaveChangesAsync();

                return Ok(new BooksResponse(newBook));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BooksResponse bookRes)
        {
            try
            {
                var fingBook = await context.Books.FirstOrDefaultAsync(x => x.IdBooks == bookRes.IdBooks);
                if (fingBook == null)
                    return BadRequest("Книга не найдена!");

                var findBookISNB = await context.Books.FirstOrDefaultAsync(x => x.Isbn == bookRes.Isbn);
                if (findBookISNB != null)
                    return BadRequest("Книга с таким ISBN уже существует!");

                fingBook.Isbn = bookRes.Isbn;
                fingBook.Name = bookRes.Name;
                fingBook.IdAutor = bookRes.IdAutor;
                fingBook.IdPublisher = bookRes.IdPublisher;
                fingBook.DatePublished = bookRes.DatePublished;
                fingBook.IdTypeCover = bookRes.IdTypeCover;
                fingBook.Language = bookRes.Language;
                fingBook.CountPages = bookRes.CountPages;
                fingBook.Desctiprtion = bookRes.Desctiprtion;
                fingBook.Cost = bookRes.Cost;

                context.Books.Update(fingBook);
                await context.SaveChangesAsync();

                return Ok(new BooksResponse(fingBook));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var findBook = await context.Books.FirstOrDefaultAsync(x => x.IdBooks == id);
                if (findBook == null)
                    return BadRequest("Книга не найдена!");

                context.Books.Remove(findBook);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

    }
}
