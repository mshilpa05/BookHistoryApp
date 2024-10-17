using Application.Interfaces;
using Application.Models;
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
        public async Task<PagedResult<ChangeHistory>> GetHistoriesByBookIdAsync(string bookId, ChangeHistoryParameters changeHistoryParameters)
        {
            var query = _context.ChangeHistories.Where(ch => ch.BookId == bookId).AsQueryable();
            var totalRecords = await query.CountAsync();
            var histories = await query
                .Skip((changeHistoryParameters.PageNumber - 1) * changeHistoryParameters.PageSize)
                .Take(changeHistoryParameters.PageSize)
                .ToListAsync();

            return new PagedResult<ChangeHistory>
            {
                Items = histories,
                TotalRecords = totalRecords,
                Page = changeHistoryParameters.PageNumber,
                PageSize = changeHistoryParameters.PageSize
            };
        }

        public async Task SaveChangeHistoryAsync(ChangeHistory changeHistory)
        {
            await _context.ChangeHistories.AddAsync(changeHistory);
            await _context.SaveChangesAsync();
        }
    }
}
