using AutoMapper;

namespace CaesarsTest.API.Mappings
{
    public class GuestMapping : Profile
    {
        public GuestMapping()
        {
            CreateMap<Entities.Guest, Models.GuestDto>().ReverseMap();

            CreateMap<Entities.Guest, Models.GuestCreateUpdateDto>().ReverseMap();
        }
    }
}
