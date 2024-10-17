using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChangeHistoryRepository
    {
        Task<List<ChangeHistory>> GetHistoriesByBookIdAsync(string bookId);
        Task SaveChangeHistoryAsync(ChangeHistory changeHistory);
    }
}
