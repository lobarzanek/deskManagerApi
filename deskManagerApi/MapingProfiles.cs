#region Usings

using AutoMapper;
using deskManagerApi.Entities.DTO.Create;
using deskManagerApi.Entities.DTO.Get;
using deskManagerApi.Entities.DTO.Update;
using deskManagerApi.Models;

#endregion

namespace deskManagerApi.Helpers
{
    /// <summary>
    /// Provides mapping profiles for data transfer objects.
    /// </summary>
    public class MapingProfiles : Profile
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MapingProfiles"/> class.
        /// </summary>
        public MapingProfiles()
        {
            // Models to Get DTO
            CreateMap<Brand, GetBrandDto>();
            CreateMap<Building, GetBuildingDto>();
            CreateMap<Desk, GetDeskDto>();
            CreateMap<DeskStatus, GetDeskStatusDto>();
            CreateMap<Floor, GetFloorDto>();
            CreateMap<Issue, GetIssueDto>();
            CreateMap<Item, GetItemDto>();
            CreateMap<Room, GetRoomDto>();
            CreateMap<Team, GetTeamDto>();

            // Create DTO to Model
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<CreateBuildingDto, Building>();
            CreateMap<CreateDeskDto, Desk>();
            CreateMap<CreateDeskStatusDto, DeskStatus>();
            CreateMap<CreateFloorDto, Floor>();
            CreateMap<CreateIssueDto, Issue>();
            CreateMap<CreateItemDto, Item>();
            CreateMap<CreateRoomDto, Room>();
            CreateMap<CreateTeamDto, Team>();

            // Update DTO to Model
            CreateMap<UpdateBrandDto, Brand>();
            CreateMap<UpdateBuildingDto, Building>();
            CreateMap<UpdateDeskDto, Desk>();
            CreateMap<UpdateDeskStatusDto, DeskStatus>();
            CreateMap<UpdateFloorDto, Floor>();
            CreateMap<UpdateIssueDto, Issue>();
            CreateMap<UpdateItemDto, Item>();
            CreateMap<UpdateRoomDto, Room>();
            CreateMap<UpdateTeamDto, Team>();

        }

        #endregion

    }
}
