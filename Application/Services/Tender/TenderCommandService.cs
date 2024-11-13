using Application.DTOs.Tender;
using Application.Interfaces.Tender;
using Domain.Models.Tender.DTO;
using Domain.Models.Tender.Value_Object;
using Domain.Repositories;

namespace Application.Services.Tender;

public class TenderCommandService : ITenderCommandService
{
    private readonly ITenderRepository _tenderRepository;

    public TenderCommandService(ITenderRepository tenderRepository)
    {
        _tenderRepository = tenderRepository;
    }

    public async Task<TenderResponseWithMessage> CreateTenderAsync(TenderRequest request)
    {
        // Validate the request
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), " درخواست نمی تواند خالی باشد");
        }

        // Create a new TenderDate object
        var tenderDate = new TenderDate(request.TenderDate.StartDate, request.TenderDate.EndDate);

        if (!tenderDate.IsValid())
        {
            throw new ArgumentException("تاریخ شروع و پایان مناقصه نامعتبر است");
        }

        // Create a new Budget object
        var budget = new Budget(request.Budget.BigAmount, request.Budget.SmallAmount);

        if (!budget.CheckAmount())
        {
            throw new ArgumentException("مبلغ مناقصه نامعتبر است");
        }

        // Create a new Tender object
        var tender = new Domain.Models.Tender.Tender(request.Title, request.Description, tenderDate, budget);

        // Add the new tender to the repository
        var addedTender = await _tenderRepository.AddTenderAsync(tender);

        // Create a new TenderResponse object
        var response = new TenderResponseWithMessage(addedTender.Title, addedTender.Description, addedTender.TenderDate, addedTender.Budget, "مناقصه با موفقیت ایجاد شد");

        return response;
    }

    public async Task<List<TenderResponseWithActiveStatus>> GetAllTendersAsync()
    {
        var getAllTender = await _tenderRepository.GetAllTenderAsync();
        return getAllTender.Select(t => new TenderResponseWithActiveStatus(t.Title, t.Description, t.TenderDate, t.Budget, t.TenderDate.IsActive())).ToList();

    }

    public async Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync()
    {
        var tendersWithDetails = await _tenderRepository.GetInProcessTendersWithDetailsAsync();
        return tendersWithDetails.Where(t => t.TenderDate.IsActive()).ToList();
    }
}
