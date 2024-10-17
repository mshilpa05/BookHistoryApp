using Application.Interfaces;
using Domain.Entities;

namespace BookHistoryApp.src.Application.Services
{
    public class ChangeHistoryService : IChangeHistoryService
    {
        private readonly IChangeHistoryRepository _changeHistoryRepository;

        public ChangeHistoryService(IChangeHistoryRepository changeHistoryRepository)
        {
            _changeHistoryRepository = changeHistoryRepository;
        }

        public async Task<List<ChangeHistory>> GetChangeHistoriesByBookIdAsync(string bookId)
        {
            return await _changeHistoryRepository.GetHistoriesByBookIdAsync(bookId);
        }
    }
}
