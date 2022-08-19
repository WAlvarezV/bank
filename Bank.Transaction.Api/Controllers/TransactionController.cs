using Bank.Common.Application.Enum;
using Bank.Common.Utilities;
using Bank.Transaction.Application.DTOs;
using Bank.Transaction.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Transaction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _service;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var response = await _service.GetTransactionByIdAsync(id);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransactionAsync([FromBody] CreateTransactionDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ApiMessage.ModelErrors(ModelState, "Bank.Transaction.Api"));
                return BadRequest(ModelState);
            }
            var response = await _service.CreateTransactionAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTransactionAsync(int id)
        {
            var response = await _service.DeleteTransactionAsync(id);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }
    }
}