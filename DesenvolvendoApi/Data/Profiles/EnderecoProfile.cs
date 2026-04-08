using AutoMapper;
using DesenvolvendoApi.Data.Dtos.Endereco;
using DesenvolvendoApi.Models;

namespace DesenvolvendoApi.Data.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDto, Endereco>();
        CreateMap<UpdateEnderecoDto, Endereco>();
        CreateMap<Endereco, ReadEnderecoDto>();

        CreateMap<UpdateEnderecoParcialDto, Endereco>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}