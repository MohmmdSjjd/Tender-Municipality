using Application.DTOs.Bid;
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

        public BidController(IBidCommandService bidCommandService)
        {
            _bidCommandService = bidCommandService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Contractor")]
        public async Task<IActionResult> CreateBidAsync([FromBody] BidRequest request)
        {
            var result = await _bidCommandService.CreateBidAsync(request);
            return Ok(result);
        }
    }
}
