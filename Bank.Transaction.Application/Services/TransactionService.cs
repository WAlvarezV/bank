using AutoMapper;
using Bank.Common.Application.DTOs;
using Bank.Common.Application.Enum;
using Bank.Common.Utilities;
using Bank.Transaction.Application.DTOs;
using Bank.Transaction.Application.Interfaces;
using Bank.Transaction.Persistence;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Bank.Transaction.Application.Services
{
    internal class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private static HttpClient _httpClient;

        public TransactionService(ILogger<TransactionService> logger, IUnitOfWork uow, IMapper mapper)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
            ConfigureHttpClient();
        }
        private void ConfigureHttpClient()
        {
            _httpClient = new()
            {
                BaseAddress = new Uri("https://localhost:7026/account/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ResponseDto> CreateTransactionAsync(CreateTransactionDto dto)
        {
            var response = new ResponseDto();
            try
            {
                var accountResponse = await _httpClient.GetAsync(dto.CuentaId.ToString());
                var jsonData = await accountResponse.Content.ReadAsStringAsync();
                if (accountResponse.IsSuccessStatusCode)
                {
                    var accountResponseDto = jsonData.DeserializeTo<ResponseDto>();

                    var jsonAccountDto = JsonSerializer.Serialize(accountResponseDto.Data);
                    var accountDto = jsonAccountDto.DeserializeTo<AccountDto>();
                    var newTransaction = new Domain.Entities.Transaction();
                    newTransaction.AccountId = dto.CuentaId;
                    newTransaction.TransactionType = dto.TipoCuenta;
                    if (dto.TipoCuenta.Equals(TransactionEnum.Debito))
                    {
                        if (accountDto.Saldo.Equals(0))
                        {
                            response.Message = "No cuenta con saldo enla cuenta";
                            response.Code = Code.Unknown;
                        }
                        else if (dto.Valor > accountDto.Saldo)
                        {
                            response.Message = "No cuenta con saldo suficiente para el debito";
                            response.Code = Code.Unknown;
                        }
                        else
                        {
                            newTransaction.InitialBalance = accountDto.Saldo;
                            accountDto.Saldo -= dto.Valor;
                            newTransaction.Balance = accountDto.Saldo;
                        }
                    }
                    else
                    {
                        newTransaction.InitialBalance = accountDto.Saldo;
                        accountDto.Saldo += dto.Valor;
                        newTransaction.Balance = accountDto.Saldo;
                    }

                    newTransaction.Value = dto.Valor;
                    newTransaction.State = true;
                    newTransaction = await _uow.Transactions.AddAsync(newTransaction);
                    if (await _uow.SaveAsync())
                    {
                        var update = new UpdateAccountDto { AccountId = accountDto.Id, Valor = accountDto.Saldo };
                        var content = new StringContent(JsonSerializer.Serialize(update), Encoding.UTF8, "application/json");
                        var updateResponse = await _httpClient.PutAsync(new Uri("https://localhost:7026/account/"), content);
                        response.Message = "Movimiento creado exitosamente";
                        response.Code = Code.Ok;
                    }
                    else
                    {
                        response.Message = "Ocurrió un error al registrar la solicitud";
                        response.Code = Code.Unknown;
                    }
                }
                else
                {
                    response.Message = jsonData;
                    response.Code = Code.Unknown;
                    return response;
                }
            }
            catch (Exception ex)
            {
                var error = $"CreateTransactionAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> DeleteTransactionAsync(int id)
        {
            var response = new ResponseDto();
            try
            {
                var Transaction = await _uow.Transactions.GetByIdAsync(id);
                if (Transaction != null)
                {
                    Transaction.State = false;
                    if (await _uow.SaveAsync())
                    {
                        response.Message = "Transactione eliminado exitosamente";
                        response.Code = Code.Ok;
                    }
                    else
                    {
                        response.Message = "Ocurrió un error al registrar la solicitud";
                        response.Code = Code.Unknown;
                    }
                }
                else
                {
                    response.Message = "Transactione no encontrado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"DeleteTransactionAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> GetTransactionByIdAsync(int id)
        {
            var response = new ResponseDto();
            try
            {
                var Transactions = await _uow.Transactions.GetAsync(x => x.Id.Equals(id));
                if (Transactions.Any())
                {
                    var Transaction = Transactions.First();
                    var TransactionDto = _mapper.Map<TransactionDto>(Transaction);
                    response.Message = "Cuenta consultada";
                    response.Code = Code.Ok;
                    response.Data = TransactionDto;
                }
                else
                {
                    response.Message = "No existen cuentas registrados con el id solicitado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetTransactionByIdAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }
        public async Task<ResponseDto> GetClientTransactionsAsync(GetListDto dto)
        {
            var response = new ResponseDto();
            try
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7026/clientaccounts/");
                var clientResponse = await _httpClient.GetAsync(dto.ClientId.ToString());
                var jsonData = await clientResponse.Content.ReadAsStringAsync();
                if (clientResponse.IsSuccessStatusCode)
                {
                    var clientResponseDto = jsonData.DeserializeTo<ResponseDto>();
                    var jsonClientDto = JsonSerializer.Serialize(clientResponseDto.Data);
                    var clientDto = jsonClientDto.DeserializeTo<ClientDto>();
                    if (clientDto.Cuentas.Any())
                    {
                        var transactions = new List<TransactionDto>();
                        foreach (var accountDto in clientDto.Cuentas)
                        {
                            var accountTransactions = await _uow.Transactions.GetAsync(x => x.AccountId.Equals(accountDto.Id));
                            if (accountTransactions.Any())
                            {
                                foreach (var transaction in accountTransactions)
                                {
                                    transactions.Add(new TransactionDto
                                    {
                                        Fecha = transaction.Date,
                                        Cliente = clientDto.NombreCompleto,
                                        NumeroCuenta = accountDto.NumeroCuenta,
                                        Tipo = accountDto.TipoCuenta.ToString(),
                                        SaldoInicial = transaction.InitialBalance,
                                        Estado = transaction.State,
                                        Movimiento = transaction.TransactionType.Equals(TransactionEnum.Debito)
                                        ? transaction.Value * -1
                                        : transaction.Value,
                                        SaldoDisponible = transaction.Balance
                                    });
                                }
                            }
                        }
                        response.Message = "Consulta exitosamente";
                        response.Code = Code.Ok;
                        response.Data = transactions;
                    }
                }
                else
                {
                    response.Message = jsonData;
                    response.Code = Code.Unknown;
                    return response;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetClientTransactionsAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }
    }
}
