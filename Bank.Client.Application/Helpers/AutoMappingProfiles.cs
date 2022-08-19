using AutoMapper;
using Bank.Client.Application.DTOs;
using Bank.Client.Domain.Entities;
using Bank.Common.Application.DTOs;

namespace Bank.Client.Application.Helpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<CreateClientDto, Person>()
                .ForMember(dest => dest.Identification, o => o.MapFrom(src => src.Identificacion))
                .ForMember(dest => dest.FullName, o => o.MapFrom(src => src.NombreCompleto))
                .ForMember(dest => dest.Gender, o => o.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Age, o => o.MapFrom(src => src.Edad))
                .ForMember(dest => dest.Address, o => o.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Phone, o => o.MapFrom(src => src.Telefono));

            CreateMap<Domain.Entities.Client, ClientDto>()
                .ForMember(dest => dest.Identificacion, o => o.MapFrom(src => src.Person.Identification))
                .ForMember(dest => dest.NombreCompleto, o => o.MapFrom(src => src.Person.FullName))
                .ForMember(dest => dest.Genero, o => o.MapFrom(src => src.Person.Gender))
                .ForMember(dest => dest.Edad, o => o.MapFrom(src => src.Person.Age))
                .ForMember(dest => dest.Direccion, o => o.MapFrom(src => src.Person.Address))
                .ForMember(dest => dest.Telefono, o => o.MapFrom(src => src.Person.Phone))
                .ForMember(dest => dest.ClienteId, o => o.MapFrom(src => src.ClientId));
        }
    }
}
