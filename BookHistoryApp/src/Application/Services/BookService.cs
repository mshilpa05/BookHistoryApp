using AutoMapper;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
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

        public async Task UpdateBook(string id, BookDTO bookUpdateDTO)
        {
            var book = _mapper.Map<Book>(bookUpdateDTO);
            book.Id = id;
           
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBook(string id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
