using Bank.Client.Application.DTOs;
using Bank.Client.Application.Interfaces;
using Bank.Client.Persistence;
using Bank.Common.Application.DTOs;
using Bank.Common.Application.Enum;
using Bank.Common.Security;
using Microsoft.Extensions.Logging;

namespace Bank.Client.Application.Services
{
    internal class CredentialService : ICredentialService
    {
        private readonly ILogger<CredentialService> _logger;
        private readonly IUnitOfWork _uow;

        public CredentialService(ILogger<CredentialService> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<ResponseDto> ValidateClientCredentialsAsync(CredentialClientDto dto)
        {
            var response = new ResponseDto();
            try
            {
                var clients = await _uow.Clients.GetAsync(x => x.ClientId.Equals(dto.ClienteId));
                if (clients.Any())
                {
                    var client = clients.First();
                    response.Message = "Cliente consultado";
                    response.Code = Code.Ok;
                    response.Data = Encryptor.Encrypt(dto.Clave).Equals(client.Password);
                }
                else
                {
                    response.Message = "No existen clientes registrados con el id solicitado";
                    response.Code = Code.Unknown;
                    response.Data = false;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetClientByIdAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
                response.Data = false;
            }
            return response;
        }
    }
}
