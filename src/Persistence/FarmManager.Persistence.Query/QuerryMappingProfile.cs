using AutoMapper;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.DataModels.Store;

namespace FarmManager.Persistence.Query;

public class QuerryMappingProfile : Profile
{
    public QuerryMappingProfile()
    {
        CreateMap<AnimalDataModel, AnimalViewModel>(MemberList.Destination).ReverseMap();

        CreateMap<CowDataModel, CowViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Animal.Id))
            .ForMember(dest => dest.RegisterNumber, opt => opt.MapFrom(src => src.Animal.RegisterNumber))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Animal.Age))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Animal.Weight))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Animal.Type))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Animal.Birthday))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsPregnant, opt => opt.MapFrom(src => src.IsPregnant))
            .ForMember(dest => dest.HasCalf, opt => opt.MapFrom(src => src.HasCalf))
            .ForMember(dest => dest.IsMilking, opt => opt.MapFrom(src => src.IsMilking));

        CreateMap<CalfDataModel, CalfViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Animal.Id))
            .ForMember(dest => dest.RegisterNumber, opt => opt.MapFrom(src => src.Animal.RegisterNumber))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Animal.Age))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Animal.Weight))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Animal.Type))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Animal.Birthday))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.MotherNumber, opt => opt.MapFrom(src => src.MotherNumber));

        CreateMap<BullDataModel, BullViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Animal.Id))
            .ForMember(dest => dest.RegisterNumber, opt => opt.MapFrom(src => src.Animal.RegisterNumber))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Animal.Age))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Animal.Weight))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Animal.Type))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Animal.Birthday))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
