using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChangeHistoryService
    {
        Task<PagedResult<ChangeHistory>> GetChangeHistoriesByBookIdAsync(string bookId, ChangeHistoryParameters changeHistoryParameters);
    }
}
