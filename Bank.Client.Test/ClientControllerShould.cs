using Bank.Client.Api.Controllers;
using Bank.Client.Application.DTOs;
using Bank.Client.Application.Interfaces;
using Bank.Common.Application.DTOs;
using Bank.Common.Application.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Bank.Client.Test
{
    public class ClientControllerShould
    {
        private readonly Mock<ILogger<ClientController>> _logger;
        private readonly Mock<IClientService> _service;
        private readonly ClientController _controller;
        public ClientControllerShould()
        {
            _logger = new Mock<ILogger<ClientController>>();
            _service = new Mock<IClientService>();
            _controller = new ClientController(_logger.Object, _service.Object);
        }

        [Fact]
        public async void Post_Should_Return_BadRequestResult()
        {
            var dto = new CreateClientDto();
            var responseDto = new ResponseDto
            {
                Code = Code.Fail,
                Message = "Fail",
                Data = dto
            };

            _service.Setup(p => p.CreateClientAsync(dto)).ReturnsAsync(responseDto);
            var result = await _controller.CreateClientAsync(dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async void Post_Should_Return_Success_Response()
        {
            var dto = new CreateClientDto();
            var responseDto = new ResponseDto
            {
                Code = Code.Ok,
                Message = "Created",
                Data = dto
            };

            _service.Setup(p => p.CreateClientAsync(dto)).ReturnsAsync(responseDto);
            var result = await _controller.CreateClientAsync(dto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseDto>(okResult.Value);
            Assert.Equal(Code.Ok, returnValue.Code);
        }
    }
}