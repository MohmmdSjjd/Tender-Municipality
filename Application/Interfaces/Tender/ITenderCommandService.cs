﻿using Application.DTOs.Tender;
using Domain.Models.Tender.DTO;

namespace Application.Interfaces.Tender
{
    public interface ITenderCommandService
    {
        Task<TenderResponseWithMessage> CreateTenderAsync(TenderRequest request);
        Task<List<TenderResponseWithActiveStatus>> GetAllTendersAsync();
        Task<List<TenderWithDetails>> GetInProcessTendersWithDetailsAsync();
    }

    

   
}
