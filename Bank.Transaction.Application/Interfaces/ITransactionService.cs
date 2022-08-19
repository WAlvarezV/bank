using Bank.Common.Application.DTOs;
using Bank.Transaction.Application.DTOs;

namespace Bank.Transaction.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<ResponseDto> GetTransactionByIdAsync(int id);
        Task<ResponseDto> GetClientTransactionsAsync(GetListDto dto);
        Task<ResponseDto> CreateTransactionAsync(CreateTransactionDto dto);
        Task<ResponseDto> DeleteTransactionAsync(int id);

    }
}
