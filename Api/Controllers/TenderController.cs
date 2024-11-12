using Application.DTOs.Tender;
using Application.Interfaces.Tender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderCommandService _commandService;

        public TenderController(ITenderCommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTenderAsync(TenderRequest request)
        {
            var response = await _commandService.CreateTenderAsync(request);
            return Ok(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTendersAsync()
        {
            var tenders = await _commandService.GetAllTendersAsync();
            return Ok(tenders);
        }

        [HttpGet("getInProcessTenders")]
        public async Task<IActionResult> GetInProcessTendersWithDetailsAsync()
        {
            var tenders = await _commandService.GetInProcessTendersWithDetailsAsync();
            return Ok(tenders);
        }
    }
}
