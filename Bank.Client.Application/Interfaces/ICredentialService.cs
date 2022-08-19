using Bank.Client.Application.DTOs;
using Bank.Common.Application.DTOs;

namespace Bank.Client.Application.Interfaces
{
    public interface ICredentialService
    {
        Task<ResponseDto> ValidateClientCredentialsAsync(CredentialClientDto dto);
    }
}
