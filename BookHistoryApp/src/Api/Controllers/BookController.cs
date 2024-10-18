using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IChangeHistoryService _changeHistoryService;
        private readonly ILogger _logger;

        public BookController(IBookService bookService, IChangeHistoryService changeHistoryService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _changeHistoryService = changeHistoryService;
            _logger = logger;   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            try
            {
                var bookDto = await _bookService.GetBookByIdAsync(id);

                if (bookDto == null)
                {
                    _logger.LogWarning("Requested resource not found.");
                    return NotFound();
                }

                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the request.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var bookDtos = await _bookService.GetAllBookAsync();

                if (bookDtos == null)
                {
                    _logger.LogWarning("Database is empty");
                    return NotFound();
                }

                _logger.LogInformation("Request handled successfully.");
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the request.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO bookCreateDTO)
        {
            try
            {
                var bookId = await _bookService.CreateBook(bookCreateDTO);

                return Created(nameof(GetBookById), new
                {
                    Message = "Resource created successfully.",
                    Data = bookId.ToString()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the creation of book.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, BookDTO bookUpdateDTO)
        {
            try
            {
                if (bookUpdateDTO == null)
                {
                    return BadRequest("Request body cannot be empty");
                }

                var updated = await _bookService.UpdateBook(id, bookUpdateDTO);

                if (updated)
                {
                    return Ok(new { Message = "Resource updated successfully." });
                }

                return NotFound(new { Message = "Resource not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the request.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                var isDeleted = await _bookService.DeleteBook(id);

                if (isDeleted)
                {
                    return NoContent();
                }

                return NotFound(new { Message = "Resource not found." });
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An error occurred during the request.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}/change-history")]
        public async Task<IActionResult> GetChangeHistory(string id, [FromQuery] ChangeHistoryParameters changeHistoryParameters)
        {
            try
            {
                if (changeHistoryParameters.EndYear < changeHistoryParameters.StartYear)
                {
                    return BadRequest("EndYear cannot be earlier than start year");
                }

                var histories = await _changeHistoryService.GetChangeHistoriesByBookIdAsync(id, changeHistoryParameters);

                if (histories == null)
                {
                    _logger.LogWarning("Requested resource not found.");
                    return NotFound();
                }

                _logger.LogInformation("Request handled successfully.");
                return Ok(histories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the request.");
                return StatusCode(500, "An internal server error occurred.");
            }
            
        }

        [HttpGet("change-history")]
        public async Task<IActionResult> GetChangeHistoryCountGroupedByBookId()
        {
            try
            {
                var changeHistories = await _changeHistoryService.GetChangeHistoryCountGroupedByBookIdAsync();
                if (changeHistories == null)
                {
                    _logger.LogWarning("Requested resource not found.");
                    return NotFound();
                }

                _logger.LogInformation("Request handled successfully.");
                return Ok(changeHistories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the request.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
