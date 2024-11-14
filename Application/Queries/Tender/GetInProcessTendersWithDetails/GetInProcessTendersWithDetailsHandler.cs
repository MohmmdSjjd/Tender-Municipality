using Application.DTOs.Tender;
using Domain.Models.Tender.DTO;
using Domain.Repositories;

namespace Application.Queries.Tender.GetInProcessTendersWithDetails
{
    public class GetInProcessTendersWithDetailsHandler : IGetInProcessTendersWithDetailsHandler
    {
        private readonly ITenderRepository _tenderRepository;

        public GetInProcessTendersWithDetailsHandler(ITenderRepository tenderRepository)
        {
            _tenderRepository = tenderRepository;
        }

        public async Task<List<TenderWithDetails>> HandleAsync()
        {
            var tendersWithDetails = await _tenderRepository.GetInProcessTendersWithDetailsAsync();
            return tendersWithDetails.Where(t => t.TenderDate.IsActive()).ToList();
        }
    }
}
