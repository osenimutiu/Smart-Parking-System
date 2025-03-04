using AutoMapper;
using SmartParkingSystem.Entities.DataTransferObjects;
using SmartParkingSystem.Entities.Models;

namespace SmartParkingSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            
            CreateMap<ParkingOwner, ParkingOwnerDto>();
            CreateMap<ParkingOwnerDto, ParkingOwner>();
            CreateMap<ParkingSpace, ParkingSpaceDto>();
            CreateMap<ParkingSpaceDto, ParkingSpace>();
            CreateMap<ParkingSpaceVM, ParkingSpace>();
            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>();
            CreateMap<Driver, DriverDto>();
            CreateMap<DriverDto, Driver>();
        }
    }
}
