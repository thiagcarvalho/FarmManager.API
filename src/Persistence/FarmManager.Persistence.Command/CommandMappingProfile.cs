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
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsMilking, opt => opt.MapFrom(src => src.IsMilking))
            .ForMember(dest => dest.IsPregnant, opt => opt.MapFrom(src => src.IsPregnant))
            .ForMember(dest => dest.HasCalf, opt => opt.MapFrom(src => src.HasCalf))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Animal, opt => opt.Ignore());

        CreateMap<Calf, CalfDataModel>()
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.MotherNumber, opt => opt.MapFrom(src => src.MotherNumber))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Animal, opt => opt.Ignore());

        CreateMap<Bull, BullDataModel>()
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Animal, opt => opt.Ignore());

        CreateMap<Lote, LoteDataModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
