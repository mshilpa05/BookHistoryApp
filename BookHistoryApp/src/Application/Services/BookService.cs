using AutoMapper;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IChangeHistoryRepository _changeHistoryRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IChangeHistoryRepository changeHistoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _changeHistoryRepository = changeHistoryRepository;
            _mapper = mapper;
        }

        public async Task<BookViewDTO?> GetBookByIdAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
                return null;

            return _mapper.Map<BookViewDTO>(book);
        }

        public async Task<IEnumerable<BookViewDTO>?> GetAllBookAsync()
        {
            var book = await _bookRepository.GetAllAsync();

            if (book == null)
                return null;

            return book.Select(book => _mapper.Map<BookViewDTO>(book));
        }

        public async Task CreateBook(BookDTO bookCreateDTO)
        {
            var book = _mapper.Map<Book>(bookCreateDTO);
            book.Id = Guid.NewGuid().ToString();

            await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateBook(string id, BookDTO bookUpdateDTO)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            book.Update(bookUpdateDTO.Title, bookUpdateDTO.Author, bookUpdateDTO.Description, bookUpdateDTO.PublishDate);

            await _bookRepository.UpdateAsync(book);

            foreach (var change in book.ChangeHistories)
            {
                await _changeHistoryRepository.SaveChangeHistoryAsync(change);
            }

            return true;
        }

        public async Task DeleteBook(string id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
