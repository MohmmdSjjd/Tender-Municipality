using Application.DTOs.Tender;
using Application.Exceptions;
using Application.Interfaces.Tender;
using Domain.Models.Tender.DTO;
using Domain.Models.Tender.Value_Object;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

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
            throw new ApiException("تاریخ شروع و پایان مناقصه نامعتبر است", StatusCodes.Status400BadRequest);
        }

        // Create a new Budget object
        var budget = new Budget(request.Budget.BigAmount, request.Budget.SmallAmount);

        if (!budget.CheckAmount())
        {
            throw new ApiException("مبلغ مناقصه نامعتبر است", StatusCodes.Status400BadRequest);
        }

        // Create a new Tender object
        var tender = new Domain.Models.Tender.Tender(request.Title, request.Description, tenderDate, budget);

        // Add the new tender to the repository
        var addedTender = await _tenderRepository.AddTenderAsync(tender);

        // Create a new TenderResponse object
        var response = new TenderResponseWithMessage(addedTender.Id, addedTender.Title, addedTender.Description, addedTender.TenderDate, addedTender.Budget, "مناقصه با موفقیت ایجاد شد");

        return response;
    }
}
