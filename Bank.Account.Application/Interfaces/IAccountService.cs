using Bank.Account.Application.DTOs;
using Bank.Common.Application.DTOs;

namespace Bank.Account.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDto> GetAllAccountsAsync();
        Task<ResponseDto> GetAccountsAsync(GetListDto dto);
        Task<ResponseDto> GetAccountByIdAsync(int id);
        Task<ResponseDto> CreateAccountAsync(CreateAccountDto dto);
        Task<ResponseDto> UpdateAccountAsync(UpdateAccountDto dto);
        Task<ResponseDto> DeleteAccountAsync(int id);
        Task<ResponseDto> GetClientAccountsAsync(int clientId);
    }
}
