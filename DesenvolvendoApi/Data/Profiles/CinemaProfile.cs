

using AutoMapper;
using DesenvolvendoApi.Data.Dtos.Cinema;
using DesenvolvendoApi.Models;

namespace DesenvolvendoApi.Data.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();

        CreateMap<Cinema, ReadCinemaDto>();

        CreateMap<UpdateCinemaParcialDto, Cinema>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}