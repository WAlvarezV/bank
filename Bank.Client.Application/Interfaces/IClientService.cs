using Bank.Client.Application.DTOs;
using Bank.Common.Application.DTOs;

namespace Bank.Client.Application.Interfaces
{
    public interface IClientService
    {
        Task<ResponseDto> GetAllClientsAsync();
        Task<ResponseDto> GetClientsAsync(GetListDto dto);
        Task<ResponseDto> GetClientByIdAsync(int id);
        Task<ResponseDto> CreateClientAsync(CreateClientDto dto);
        Task<ResponseDto> UpdateClientAsync(UpdateClientDto dto);
        Task<ResponseDto> DeleteClientAsync(int id);
    }
}
