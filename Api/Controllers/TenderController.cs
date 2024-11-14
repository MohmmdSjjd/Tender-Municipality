using Application.Commands.Tender.CreateTender;
using Application.Interfaces.Tender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ICreateTenderCommandHandler _createTenderCommandHandler;
        private readonly ITenderQueryService _queryService;

        public TenderController(ICreateTenderCommandHandler createTenderCommandHandler,ITenderQueryService queryService)
        {
            _createTenderCommandHandler = createTenderCommandHandler;
            _queryService = queryService;
        }   

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTenderAsync(CreateTenderCommand request)
        {
            var response = await _createTenderCommandHandler.HandleAsync(request);
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
