using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<BookViewDTO?> GetBookByIdAsync(string id);

        Task<IEnumerable<BookViewDTO>?> GetAllBookAsync();

        Task CreateBook(BookDTO bookCreateDTO);
        Task<bool> UpdateBook(string id, BookDTO bookUpdateDTO);
        Task DeleteBook(string id);

    }
}
