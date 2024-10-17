using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookHistoryApp.src.Infrastructure
{
    public class ChangeHistoryRepository : IChangeHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ChangeHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ChangeHistory>> GetHistoriesByBookIdAsync(string bookId)
        {
            return await _context.ChangeHistories.Where(ch => ch.BookId == bookId).ToListAsync();
        }

        public async Task SaveChangeHistoryAsync(ChangeHistory changeHistory)
        {
            await _context.ChangeHistories.AddAsync(changeHistory);
            await _context.SaveChangesAsync();
        }
    }
}
