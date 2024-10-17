using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChangeHistoryRepository
    {
        Task<PagedResult<ChangeHistory>> GetHistoriesByBookIdAsync(string bookId, ChangeHistoryParameters changeHistoryParameters);
        Task SaveChangeHistoryAsync(ChangeHistory changeHistory);
    }
}
