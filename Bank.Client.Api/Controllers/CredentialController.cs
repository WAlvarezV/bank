using Bank.Client.Application.DTOs;
using Bank.Client.Application.Interfaces;
using Bank.Common.Application.Enum;
using Bank.Common.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialController : ControllerBase
    {
        private readonly ILogger<CredentialController> _logger;
        private readonly ICredentialService _service;

        public CredentialController(ILogger<CredentialController> logger, ICredentialService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> ValidateClientCredentialsAsync([FromBody] CredentialClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ApiMessage.ModelErrors(ModelState, "Bank.Client.Api"));
                return BadRequest(ModelState);
            }
            var response = await _service.ValidateClientCredentialsAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }
    }
}