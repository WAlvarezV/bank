using Bank.Common.Application.Enum;
using Bank.Transaction.Application.DTOs;
using Bank.Transaction.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Transaction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientTransactionsController : ControllerBase
    {
        private readonly ILogger<ClientTransactionsController> _logger;
        private readonly ITransactionService _service;

        public ClientTransactionsController(ILogger<ClientTransactionsController> logger, ITransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> GetClientTransactionsAsync([FromBody] GetListDto dto)
        {
            var response = await _service.GetClientTransactionsAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }
    }
}