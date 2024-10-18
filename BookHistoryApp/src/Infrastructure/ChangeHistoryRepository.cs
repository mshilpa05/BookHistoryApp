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
            var query = _context.ChangeHistories
                .Where(ch => ch.BookId == bookId)
                .Where(ch => ch.ChangeDate.Year >= changeHistoryParameters.StartYear 
                            && ch.ChangeDate.Year <= changeHistoryParameters.EndYear) // filtering
                .AsQueryable();

            var totalRecords = await query.CountAsync();

            var histories = await query
                .Skip((changeHistoryParameters.PageNumber - 1) * changeHistoryParameters.PageSize)
                .Take(changeHistoryParameters.PageSize) // pagination
                .OrderBy(ch => ch.ChangeDate) // ordering
                .ToListAsync();

            return new PagedResult<ChangeHistory>
            {
                Items = histories,
                TotalRecords = totalRecords,
                Page = changeHistoryParameters.PageNumber,
                PageSize = changeHistoryParameters.PageSize
            };
        }

        public async Task<IEnumerable<ChangeHistoryGroupedByBookId>> GetCountOfChangesByBookId()
        {
            return await _context.ChangeHistories.GroupBy(a => a.BookId) // Grouping
                .Select(a => new ChangeHistoryGroupedByBookId { BookId = a.Key, ChangeHistoryLogCount = a.Count() })
                .ToListAsync();
        }
    }
}
