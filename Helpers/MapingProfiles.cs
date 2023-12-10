using AutoMapper;
using deskManagerApi.DTO.Create;
using deskManagerApi.DTO.Get;
using deskManagerApi.DTO.Update;
using deskManagerApi.Models;

namespace deskManagerApi.Helpers
{
    public class MapingProfiles : Profile
    {

        public MapingProfiles()
        {
            //Models to DTO
            CreateMap<Brand, GetBrandDto>();

            //Create DTO to Model
            CreateMap<CreateBrandDto, Brand>();

            //Update DTO to Model
            CreateMap<UpdateBrandDto, Brand>();
        }
        
    }
}
