using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService, ILogger<BookController> logger) : base(logger) 
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            try
            {
                var bookDto = await _bookService.GetBookByIdAsync(id);
                return HandleResponse(bookDto);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var bookDtos = await _bookService.GetAllBookAsync();
                return HandleResponse(bookDtos);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO bookCreateDTO)
        {
            try
            {
                await _bookService.CreateBook(bookCreateDTO);
                return HandleCreationResponse(bookCreateDTO, nameof(GetBookById));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, BookDTO bookUpdateDTO)
        {
            try
            {
                await _bookService.UpdateBook(id, bookUpdateDTO);
                return HandleUpdateResponse(true);
                // handle false cases
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                await _bookService.DeleteBook(id);
                return HandleDeletionResponse(true);
                // handle false cases
            }
            catch(Exception ex) 
            {
                return HandleError(ex);
            }
        }
    }
}
