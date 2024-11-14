using Application.DTOs.Tender;
using Domain.Repositories;

namespace Application.Queries.Tender.GetAllTenders
{
    public class GetAllTendersQueryHandler : IGetAllTendersQueryHandler
    {
        private readonly ITenderRepository _tenderRepository;

        public GetAllTendersQueryHandler(ITenderRepository tenderRepository)
        {
            _tenderRepository = tenderRepository;
        }

        public async Task<List<TenderResponseWithActiveStatus>> HandleAsync()
        {
            var getAllTender = await _tenderRepository.GetAllTenderAsync();
            
            return getAllTender.Select(t => new TenderResponseWithActiveStatus
            (t.TenderId, t.Title, t.Description, t.TenderDate, t.Budget, t.TenderDate.IsActive())
            { Bids = t.Bids, Winner = t.WinnerBid }).ToList();
        }
    }
}
