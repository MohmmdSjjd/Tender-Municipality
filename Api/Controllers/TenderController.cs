using Application.DTOs.Tender;
using Application.Interfaces.Tender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderCommandService _commandService;
        private readonly ITenderQueryService _queryService;

        public TenderController(ITenderCommandService commandService,ITenderQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTenderAsync(TenderRequest request)
        {
            var response = await _commandService.CreateTenderAsync(request);
            return Ok(response);
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTendersAsync()
        {
            var tenders = await _queryService.GetAllTendersAsync();
            return Ok(tenders);
        }

        [HttpGet("getInProcessTenders")]
        [Authorize(Roles = "Contractor")]
        public async Task<IActionResult> GetInProcessTendersWithDetailsAsync()
        {
            var tenders = await _queryService.GetInProcessTendersWithDetailsAsync();
            return Ok(tenders);
        }
    }
}
