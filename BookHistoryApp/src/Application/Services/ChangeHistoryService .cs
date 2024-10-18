using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Services
{
    public class ChangeHistoryService : IChangeHistoryService
    {
        private readonly IChangeHistoryRepository _changeHistoryRepository;

        public ChangeHistoryService(IChangeHistoryRepository changeHistoryRepository)
        {
            _changeHistoryRepository = changeHistoryRepository;
        }

        public async Task<PagedResult<ChangeHistory>> GetChangeHistoriesByBookIdAsync(string bookId, ChangeHistoryParameters changeHistoryParameters)
        {
            return await _changeHistoryRepository.GetHistoriesByBookIdAsync(bookId, changeHistoryParameters);
        }

        public async Task<IEnumerable<ChangeHistoryGroupedByBookId>> GetChangeHistoryCountGroupedByBookIdAsync()
        {
            return await _changeHistoryRepository.GetCountOfChangesByBookId();
        }
    }
}
