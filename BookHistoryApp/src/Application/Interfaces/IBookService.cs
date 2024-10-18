using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<BookViewDTO?> GetBookByIdAsync(string id);
        Task<IEnumerable<BookViewDTO>?> GetAllBookAsync();
        Task<string> CreateBook(BookDTO bookCreateDTO);
        Task<bool> UpdateBook(string id, BookDTO bookUpdateDTO);
        Task<bool> DeleteBook(string id);

    }
}
