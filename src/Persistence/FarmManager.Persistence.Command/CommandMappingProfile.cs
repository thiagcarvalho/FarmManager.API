using AutoMapper;
using FarmManager.Domain.Entities;
using FarmManager.Persistence.DataModels.Store;


namespace FarmManager.Persistence.Command;

public class CommandMappingProfile : Profile
{
    public CommandMappingProfile()
    {
        CreateMap<Animal, AnimalDataModel>()
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight.Value));
    }
}
