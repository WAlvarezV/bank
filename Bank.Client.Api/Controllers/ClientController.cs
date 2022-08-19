using Bank.Client.Application.DTOs;
using Bank.Client.Application.Interfaces;
using Bank.Common.Application.Enum;
using Bank.Common.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _service;

        public ClientController(ILogger<ClientController> logger, IClientService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var response = await _service.GetClientByIdAsync(id);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var response = await _service.GetAllClientsAsync();
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult> CreateClientAsync([FromBody] CreateClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ApiMessage.ModelErrors(ModelState, "Bank.Client.Api"));
                return BadRequest(ModelState);
            }
            var response = await _service.CreateClientAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClientAsync([FromBody] UpdateClientDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(ApiMessage.ModelErrors(ModelState, "Bank.Client.Api"));
                return BadRequest(ModelState);
            }
            var response = await _service.UpdateClientAsync(dto);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteClientAsync(int id)
        {
            var response = await _service.DeleteClientAsync(id);
            if (response.Code.Equals(Code.Ok))
                return Ok(response);
            else
                return BadRequest(response.Message);
        }
    }
}