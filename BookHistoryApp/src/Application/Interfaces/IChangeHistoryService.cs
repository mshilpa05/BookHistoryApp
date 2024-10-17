using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChangeHistoryService
    {
        Task<List<ChangeHistory>> GetChangeHistoriesByBookIdAsync(string bookId);
    }
}
