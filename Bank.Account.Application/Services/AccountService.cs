using AutoMapper;
using Bank.Account.Application.DTOs;
using Bank.Account.Application.Interfaces;
using Bank.Account.Persistence;
using Bank.Common.Application.DTOs;
using Bank.Common.Application.Enum;
using Bank.Common.Utilities;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Bank.Account.Application.Services
{
    internal class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private static HttpClient _httpClient;

        public AccountService(ILogger<AccountService> logger, IUnitOfWork uow, IMapper mapper)
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
                BaseAddress = new Uri("https://localhost:7083/client/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ResponseDto> CreateAccountAsync(CreateAccountDto dto)
        {
            var response = new ResponseDto();
            try
            {
                var clientResponse = await _httpClient.GetAsync(dto.ClienteId.ToString());
                var jsonData = await clientResponse.Content.ReadAsStringAsync();
                if (clientResponse.IsSuccessStatusCode)
                {
                    var clientResponseDto = jsonData.DeserializeTo<ResponseDto>();
                    var newAccount = new Domain.Entities.Account();
                    newAccount.ClientId = dto.ClienteId;
                    newAccount.Number = dto.NumeroCuenta;
                    newAccount.AccountType = dto.TipoCuenta;
                    newAccount.Balance = dto.SaldoInicial;
                    newAccount.State = true;
                    newAccount = await _uow.Accounts.AddAsync(newAccount);
                    if (await _uow.SaveAsync())
                    {
                        response.Message = "Cuenta creada exitosamente";
                        response.Code = Code.Ok;
                        response.Data = _mapper.Map<AccountDto>(newAccount);
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
                var error = $"CreateAccountAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> DeleteAccountAsync(int id)
        {
            var response = new ResponseDto();
            try
            {
                var Account = await _uow.Accounts.GetByIdAsync(id);
                if (Account != null)
                {
                    Account.State = false;
                    if (await _uow.SaveAsync())
                    {
                        response.Message = "Accounte eliminado exitosamente";
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
                    response.Message = "Accounte no encontrado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"DeleteAccountAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> GetAllAccountsAsync()
        {
            var response = new ResponseDto();
            try
            {
                var Accounts = await _uow.Accounts.GetAsync(x => x.State.Equals(true));
                if (Accounts.Any())
                {
                    response.Message = "Cuentas consultadas";
                    response.Code = Code.Ok;
                    response.Data = _mapper.Map<IEnumerable<AccountDto>>(Accounts);
                }
                else
                {
                    response.Message = "No existen cuentas registradas";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetAllAccountsAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> GetAccountByIdAsync(int id)
        {
            var response = new ResponseDto();
            try
            {
                var Accounts = await _uow.Accounts.GetAsync(x => x.Id.Equals(id));
                if (Accounts.Any())
                {
                    var Account = Accounts.First();
                    var AccountDto = _mapper.Map<AccountDto>(Account);
                    response.Message = "Cuenta consultada";
                    response.Code = Code.Ok;
                    response.Data = AccountDto;
                }
                else
                {
                    response.Message = "No existen cuentas registrados con el id solicitado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetAccountByIdAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public Task<ResponseDto> GetAccountsAsync(GetListDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> UpdateAccountAsync(UpdateAccountDto dto)
        {
            var response = new ResponseDto();
            try
            {
                var Account = await _uow.Accounts.GetByIdAsync(dto.AccountId);
                if (Account != null)
                {
                    Account.Balance = dto.Valor;
                    if (await _uow.SaveAsync())
                    {
                        response.Message = "Cuenta actualizada exitosamente";
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
                    response.Message = "Accounte no encontrado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"UpdateAccountAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> GetClientAccountsAsync(int clientId)
        {
            var response = new ResponseDto();
            try
            {
                var clientResponse = await _httpClient.GetAsync(clientId.ToString());
                var jsonData = await clientResponse.Content.ReadAsStringAsync();
                if (clientResponse.IsSuccessStatusCode)
                {
                    var clientResponseDto = jsonData.DeserializeTo<ResponseDto>();
                    var jsonClientDto = JsonSerializer.Serialize(clientResponseDto.Data);
                    var clientDto = jsonClientDto.DeserializeTo<ClientDto>();
                    var accounts = await _uow.Accounts.GetAsync(x => x.ClientId.Equals(clientDto.Id));
                    if (accounts.Any())
                    {
                        response.Message = "Consulta exitosamente";
                        response.Code = Code.Ok;
                        clientDto.Cuentas = _mapper.Map<IList<AccountDto>>(accounts);
                        response.Data = clientDto;
                    }
                    else
                    {
                        response.Message = $"No existen cuentas registradas para el usuario: {clientDto.NombreCompleto}";
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
                var error = $"GetClientAccountsAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }
    }
}
