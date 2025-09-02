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
        CreateMap<Cow, CowDataModel>()
            .IncludeBase<Animal, AnimalDataModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsMilking, opt => opt.MapFrom(src => src.IsMilking))
            .ForMember(dest => dest.IsPregnant, opt => opt.MapFrom(src => src.IsPregnant))
            .ForMember(dest => dest.HasCalf, opt => opt.MapFrom(src => src.HasCalf));

        CreateMap<Calf, CalfDataModel>()
            .IncludeBase<Animal, AnimalDataModel>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.MotherNumber, opt => opt.MapFrom(src => src.MotherNumber));
    }
}
