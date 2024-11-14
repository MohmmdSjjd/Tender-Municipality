using Application.Commands.Bid.CreateBid;
using Application.Interfaces;
using Application.Interfaces.Bid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidCommandService _bidCommandService;
        private readonly INotificationHub _notificationHub;

        public BidController(IBidCommandService bidCommandService, INotificationHub notificationHub)
        {
            _bidCommandService = bidCommandService;
            _notificationHub = notificationHub;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Contractor")]
        public async Task<IActionResult> CreateBidAsync([FromBody] CreateBidCommand request)
        {
            var result = await _bidCommandService.CreateBidAsync(request);

            await _notificationHub.BidCreated($"New bid created for tender: {result.TenderId}");

            return Ok(result);
        }
    }
}
