using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(string id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Book.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
