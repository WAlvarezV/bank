using AutoMapper;
using Bank.Client.Application.DTOs;
using Bank.Client.Application.Interfaces;
using Bank.Client.Domain.Entities;
using Bank.Client.Persistence;
using Bank.Common.Application.DTOs;
using Bank.Common.Application.Enum;
using Bank.Common.Security;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Bank.Client.Application.Services
{
    internal class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private static HttpClient _httpClient;

        public ClientService(ILogger<ClientService> logger, IUnitOfWork uow, IMapper mapper)
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

        public async Task<ResponseDto> CreateClientAsync(CreateClientDto dto)
        {
            var response = new ResponseDto();
            try
            {
                var person = _mapper.Map<Person>(dto);
                var newClient = new Domain.Entities.Client();
                newClient.SetPerson(person);
                newClient.SetClientId(dto.ClienteId);
                newClient.SetPassword(Encryptor.Encrypt(dto.Clave));
                newClient.SetState(true);
                newClient = await _uow.Clients.AddAsync(newClient);
                if (await _uow.SaveAsync())
                {
                    response.Message = "Cliente creado exitosamente";
                    response.Code = Code.Ok;
                    response.Data = _mapper.Map<ClientDto>(newClient);
                }
                else
                {
                    response.Message = "Ocurrió un error al registrar la solicitud";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"CreateClientAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> DeleteClientAsync(int id)
        {
            var response = new ResponseDto();
            try
            {
                var client = await _uow.Clients.GetByIdAsync(id);
                if (client != null)
                {
                    client.SetState(false);
                    if (await _uow.SaveAsync())
                    {
                        response.Message = "Cliente eliminado exitosamente";
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
                    response.Message = "Cliente no encontrado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"DeleteClientAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> GetAllClientsAsync()
        {
            var response = new ResponseDto();
            try
            {
                var clients = await _uow.Clients.GetAsync(x => x.State.Equals(true), null, "Person");
                if (clients.Any())
                {
                    response.Message = "Clientes consultados";
                    response.Code = Code.Ok;
                    response.Data = _mapper.Map<IEnumerable<ClientDto>>(clients);
                }
                else
                {
                    response.Message = "No existen clientes registrados";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetAllClientsAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public async Task<ResponseDto> GetClientByIdAsync(int id)
        {
            var response = new ResponseDto();
            try
            {
                var clients = await _uow.Clients.GetAsync(x => x.Id.Equals(id), null, "Person");
                if (clients.Any())
                {
                    var client = clients.First();
                    var clientDto = _mapper.Map<ClientDto>(client);
                    var accountResponse = await _httpClient.GetAsync(client.Id.ToString());
                    var jsonData = await accountResponse.Content.ReadAsStringAsync();
                    if (accountResponse.IsSuccessStatusCode)
                    {
                        //    var clientResponseDto = jsonData.DeserializeTo<ResponseDto>();
                        //    var newAccount = new Domain.Entities.Account();
                        //    newAccount.ClientId = dto.ClienteId;
                        //    newAccount.Number = dto.NumeroCuenta;
                        //    newAccount.AccountType = dto.TipoCuenta;
                        //    newAccount.Balance = dto.SaldoInicial;
                        //    newAccount.State = true;
                        //    newAccount = await _uow.Accounts.AddAsync(newAccount);
                        //    if (await _uow.SaveAsync())
                        //    {
                        //        response.Message = "Cuenta creada exitosamente";
                        //        response.Code = Code.Ok;
                        //        var clientDto = (ClientDto)clientResponseDto.Data;
                        //        clientDto.Cuentas.Add(_mapper.Map<AccountDto>(newAccount));
                        //        response.Data = clientDto;
                        //    }
                        //    else
                        //    {
                        //        response.Message = "Ocurrió un error al registrar la solicitud";
                        //        response.Code = Code.Unknown;
                        //    }
                        //}
                        //else
                        //{
                        //    response.Message = jsonData;
                        //    response.Code = Code.Unknown;
                        //    return response;
                        //}

                    }

                    response.Message = "Cliente consultado";
                    response.Code = Code.Ok;
                    response.Data = clientDto;
                }
                else
                {
                    response.Message = "No existen clientes registrados con el id solicitado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"GetClientByIdAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }

        public Task<ResponseDto> GetClientsAsync(GetListDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> UpdateClientAsync(UpdateClientDto dto)
        {
            var response = new ResponseDto();
            try
            {
                var client = await _uow.Clients.GetByIdAsync(dto.Id);
                if (client != null)
                {
                    client.SetPassword(Encryptor.Encrypt(dto.Clave));
                    if (await _uow.SaveAsync())
                    {
                        response.Message = "Clave actualizada exitosamente";
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
                    response.Message = "Cliente no encontrado";
                    response.Code = Code.Unknown;
                }
            }
            catch (Exception ex)
            {
                var error = $"UpdateClientAsync ExceptionError => {ex.Message} {(ex.InnerException != null ? $"InnerExceptionError => {ex.InnerException.Message}" : "")}";
                _logger.LogError(ex, error);
                response.Message = "Ocurrió un error al procesar la solicitud";
                response.Code = Code.Fail;
            }
            return response;
        }
    }
}
