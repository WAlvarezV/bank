using AutoMapper;
using Bank.Common.Application.DTOs;

namespace Bank.Account.Application.Helpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            //CreateMap<CreateAccountDto, Person>()
            //    .ForMember(dest => dest.Identification, o => o.MapFrom(src => src.Identificacion))
            //    .ForMember(dest => dest.FullName, o => o.MapFrom(src => src.NombreCompleto))
            //    .ForMember(dest => dest.Gender, o => o.MapFrom(src => src.Genero))
            //    .ForMember(dest => dest.Age, o => o.MapFrom(src => src.Edad))
            //    .ForMember(dest => dest.Address, o => o.MapFrom(src => src.Direccion))
            //    .ForMember(dest => dest.Phone, o => o.MapFrom(src => src.Telefono));

            CreateMap<Domain.Entities.Account, AccountDto>()
                .ForMember(dest => dest.ClienteId, o => o.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.NumeroCuenta, o => o.MapFrom(src => src.Number))
                .ForMember(dest => dest.TipoCuenta, o => o.MapFrom(src => src.AccountType))
                .ForMember(dest => dest.Saldo, o => o.MapFrom(src => src.Balance))
                .ForMember(dest => dest.Estado, o => o.MapFrom(src => src.State))
                .ReverseMap();
        }
    }
}
