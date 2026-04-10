using AutoMapper;
using DesenvolvendoApi.Data.Dtos.Sessao;
using DesenvolvendoApi.Models;

namespace DesenvolvendoApi.Data.Profiles;

public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDto, Sessao>();
        CreateMap<UpdateSessaoDto, Sessao>();
        CreateMap<Sessao, ReadSessaoDto>();
    }
}