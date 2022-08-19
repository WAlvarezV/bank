using AutoMapper;
using Bank.Common.Application.DTOs;

namespace Bank.Transaction.Application.Helpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Domain.Entities.Transaction, TransactionDto>()
                //.ForMember(dest => dest.ClienteId, o => o.MapFrom(src => src.ClientId))
                //.ForMember(dest => dest.NumeroCuenta, o => o.MapFrom(src => src.Number))
                //.ForMember(dest => dest.TipoCuenta, o => o.MapFrom(src => src.TransactionType))
                //.ForMember(dest => dest.Saldo, o => o.MapFrom(src => src.Balance))
                //.ForMember(dest => dest.Estado, o => o.MapFrom(src => src.State))
                .ReverseMap();
        }
    }
}
