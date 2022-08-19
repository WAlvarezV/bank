using Bank.Account.Application.Interfaces;
using Bank.Common.Application.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientAccountsController : ControllerBase
    {
        private readonly ILogger<ClientAccountsController> _logger;
        private readonly IAccountService _service;

        public ClientAccountsController(ILogger<ClientAccountsController> logger, IAccountService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult> GetClientAccountsAsync(int clientId)
        {
            var response = await _service.GetClientAccountsAsync(clientId);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }
    }
}