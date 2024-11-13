using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Bid;
using Application.Interfaces.Bid;
using Domain.Repositories;

namespace Application.Services.Bid
{
    public class BidCommandService : IBidCommandService
    {
        private readonly IBidRepository _bidRepository;
        private readonly ITenderRepository _tenderRepository;

        public BidCommandService(IBidRepository bidRepository, ITenderRepository tenderRepository)
        {
            _bidRepository = bidRepository;
            _tenderRepository = tenderRepository;
        }
        public async Task<BidResponse> CreateBidAsync(BidRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "درخواست نمی تواند خالی باشد");
            }

            var tender = await _tenderRepository.FindByIdAsync(request.TenderId);

            if (tender == null)
            {
                throw new ArgumentException("مناقصه مورد نظر یافت نشد");
            }

            var bid = new Domain.Models.Bid.Bid(request.Price, request.UserId, request.TenderId);

            if (!tender.CheckBudget(bid.Price))
            {
                throw new ArgumentException("قیمت پیشنهادی نامعتبر است");
            }

            var checkBidExist = await _bidRepository.CheckBidExist(request.Price, request.TenderId, request.UserId);

            if (checkBidExist)
            {
                throw new ArgumentException("شما قبلا پیشنهادی را با این مبلغ ثبت کردید");
            }

            var createBid = await _bidRepository.AddAsync(bid);

            if (createBid == null)
            {
                throw new ArgumentException("خطا در ایجاد پیشنهاد");
            }

            return new BidResponse(" پیشنهاد با موفقیت ایجاد شد", createBid.TenderId);
        }

    }
}
