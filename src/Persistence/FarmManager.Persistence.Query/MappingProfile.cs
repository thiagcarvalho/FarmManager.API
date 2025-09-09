using AutoMapper;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.DataModels.Store;

namespace FarmManager.Persistence.Query;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnimalDataModel, AnimalViewModel>(MemberList.Destination).ReverseMap();
        CreateMap<CowDataModel, CowViewModel>(MemberList.Destination).ReverseMap();
        CreateMap<CalfDataModel, CalfViewModel>(MemberList.Destination).ReverseMap();
        CreateMap<BullDataModel, BullViewModel>(MemberList.Destination).ReverseMap();
    }
}
