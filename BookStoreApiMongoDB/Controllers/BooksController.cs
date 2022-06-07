using BookStoreApiMongoDB.Models;
using BookStoreApiMongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApiMongoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<List<Book>> GetAll()
        {
            return await _booksService.GetAllAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> GetByIdAsync(string id)
        {
            var book = await _booksService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Book book)
        {
            await _booksService.CreateSync(book);

            return Ok(book);// CreatedAtAction(nameof(GetByIdAsync), new {id = book.Id});
        }
    
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Book updateBook)
        {
            var book = await _booksService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            updateBook.Id = book.Id;

            await _booksService.UpdateAsync(id, updateBook);

            return NoContent();

        }
    
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var book = await _booksService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _booksService.DeleteAsync(id);

            return NoContent();
        }
    
    }
}
