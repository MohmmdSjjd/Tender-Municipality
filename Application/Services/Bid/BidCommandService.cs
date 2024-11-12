using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Bid;
using Application.Interfaces.Bid;

namespace Application.Services.Bid
{
    public class BidCommandService : IBidCommandService
    {
        public Task<BidResponse> CreateBidAsync(BidRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BidResponse> UpdateBidAsync(BidRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BidResponse> DeleteBidAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
