﻿#region Usings

using AutoMapper;
using deskManagerApi.Entities.DTO.Create;
using deskManagerApi.Entities.DTO.Get;
using deskManagerApi.Entities.DTO.Update;
using deskManagerApi.Entities.Models;
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
            CreateMap<Desk, GetDeskBasicInfo>();
            CreateMap<Desk, GetDeskMapViewDto>()
                .ForMember(d => d.Status, conf => 
                conf.MapFrom(s => s.Status.ToString()));
            CreateMap<Floor, GetFloorDto>();
            CreateMap<Floor, GetFloorBasicInfo>();
            CreateMap<Issue, GetIssueDto>();
            CreateMap<Item, GetItemDto>();
            CreateMap<Room, GetRoomDto>();
            CreateMap<Room, GetRoomBasicInfo>();
            CreateMap<Room, GetRoomMapDto>()
                .ForMember(r => r.SvgMap, conf =>
                conf.MapFrom(r => new SVGMap() 
                { 
                    mapViewBox = r.mapViewBox, 
                    mapHeight = r.mapHeight, 
                    mapWidth = r.mapWidth,
                    mapXmlns = r.mapXmlns,
                }));
            CreateMap<Team, GetTeamDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<User, GetUserBasicInfo>().ForMember(d => d.Name, conf => conf.MapFrom(u => $"{u.FirstName} {u.LastName}"));

            // Create DTO to Model
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<CreateBuildingDto, Building>();
            CreateMap<CreateDeskDto, Desk>();
            CreateMap<CreateFloorDto, Floor>();
            CreateMap<CreateIssueDto, Issue>();
            CreateMap<CreateItemDto, Item>();
            CreateMap<CreateRoomDto, Room>();
            CreateMap<CreateTeamDto, Team>();
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateReservationDto, Reservation>();

            // Update DTO to Model
            CreateMap<UpdateBrandDto, Brand>();
            CreateMap<UpdateBuildingDto, Building>();
            CreateMap<UpdateDeskDto, Desk>();
            CreateMap<UpdateFloorDto, Floor>();
            CreateMap<UpdateIssueDto, Issue>();
            CreateMap<UpdateItemDto, Item>();
            CreateMap<UpdateRoomDto, Room>();
            CreateMap<UpdateTeamDto, Team>();
            CreateMap<UpdateUserDto, User>();
        }

        #endregion

    }
}
