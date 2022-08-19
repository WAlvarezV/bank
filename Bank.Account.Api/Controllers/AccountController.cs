using Bank.Account.Application.DTOs;
using Bank.Account.Application.Interfaces;
using Bank.Common.Application.DTOs;
using Bank.Common.Application.Enum;
using Bank.Common.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _service;

        public AccountController(ILogger<AccountController> logger, IAccountService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllAsync(int id)
        {
            var response = await _service.GetAccountByIdAsync(id);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await _service.GetAllAccountsAsync();
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccountAsync([FromBody] CreateAccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ApiMessage.ModelErrors(ModelState, "Bank.Account.Api"));
                return BadRequest(ModelState);
            }
            var response = await _service.CreateAccountAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAccountAsync([FromBody] UpdateAccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ApiMessage.ModelErrors(ModelState, "Bank.Account.Api"));
                return BadRequest(ModelState);
            }
            var response = await _service.UpdateAccountAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccountAsync(int id)
        {
            var response = await _service.DeleteAccountAsync(id);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }
    }
}