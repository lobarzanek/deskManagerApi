#region Usings

using AutoMapper;
using deskManagerApi.Contracts;
using deskManagerApi.Entities.DTO.Create;
using deskManagerApi.Entities.DTO.Get;
using deskManagerApi.Entities.DTO.Update;
using deskManagerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace deskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        #region Fields and Constants

        /// <summary>
        /// Value of repository wrapper.
        /// </summary>
        private readonly IRepositoryWrapper _repositoryWrapper;

        /// <summary>
        /// Value of AutoMapper instance.
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The Constructor of Floor Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public FloorController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Floors.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /floor
        ///
        /// </remarks>
        /// <response code="200">If Floor ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet(Name = "GetAllFloors")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetFloorDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllFloors()
        {
            try
            {
                var _floors = await _repositoryWrapper.Floor.GetAllFloors();

                var _floorsMap = _mapper.Map<IEnumerable<GetFloorDto>>(_floors);

                _floorsMap = await AddNamesToDtoIdArray(_floorsMap);

                return Ok(_floorsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a list of all Floors with basic info.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /floor/basic
        ///
        /// </remarks>
        /// <response code="200">If Floor ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("basic", Name = "GetAllFloorsBasicInfo")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetFloorBasicInfo>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllFloorsBasicInfo()
        {
            try
            {
                var _floors = await _repositoryWrapper.Floor.GetAllFloors();

                var _floorsMap = _mapper.Map<IEnumerable<GetFloorBasicInfo>>(_floors);

                return Ok(_floorsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Floor for provided ID.
        /// </summary>
        /// <param name="id">The Floor ID value to get the Floor object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /floor/1
        ///
        /// </remarks>
        /// <response code="200">If Floor ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetFloorById")]
        [ProducesResponseType((200), Type = typeof(GetFloorDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFloorById(int id)
        {
            try
            {
                var _floor = await _repositoryWrapper.Floor.GetFloorById(id);

                if( _floor == null )
                {
                    return NotFound();
                }

                var _floorMap = _mapper.Map<GetFloorDto>(_floor);

                return Ok(_floorMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a floor with the given parameters.
        /// </summary>
        /// <param name="floor">Value for create Floor model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /floor
        ///     
        ///     {
        ///        "name": "Floor #1",
        ///        "buildingId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the floor is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetFloorDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateFloor([FromBody]CreateFloorDto floor)
        {
            try
            {
                if (floor is null)
                {
                    return BadRequest("Floor object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if(floor.BuildingId != null)
                {
                    var _buildingEntity = await _repositoryWrapper.Building.GetBuildingById((int)floor.BuildingId);

                    if(_buildingEntity is null)
                    {
                        return BadRequest("Invalid building ID");
                    }
                }

                var _floorEntity = _mapper.Map<Floor>(floor);
                await _repositoryWrapper.Floor.CreateFloor(_floorEntity);
                await  _repositoryWrapper.Save();

                var _createdFloor = _mapper.Map<GetFloorDto>(_floorEntity);

                _createdFloor = await AddNamesToDtoId(_createdFloor);


                return CreatedAtRoute("GetFloorById", new { id = _createdFloor.Id }, _createdFloor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Floor with provided model.
        /// </summary>
        /// <param name="floor">Value for update Floor model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /floor
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Floor #1",
        ///        "buildingId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the floor is null or invalid</response>
        /// <response code="404">If the floor is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetFloorDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateFloor([FromBody] UpdateFloorDto floor)
        {
            try
            {
                if (floor is null)
                {
                    return BadRequest("Floor object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _floorEntity = await _repositoryWrapper.Floor.GetFloorById(floor.Id);

                if(_floorEntity is null)
                {
                    return NotFound();
                }

                if (floor.BuildingId != null)
                {
                    var _buildingEntity = await _repositoryWrapper.Building.GetBuildingById((int)floor.BuildingId);

                    if (_buildingEntity is null)
                    {
                        return BadRequest("Invalid building ID");
                    }
                }

                _mapper.Map(floor, _floorEntity);
                _repositoryWrapper.Floor.UpdateFloor(_floorEntity);
                await _repositoryWrapper.Save();

                var _updatedFloor = _mapper.Map<GetFloorDto>(_floorEntity);

                _updatedFloor = await AddNamesToDtoId(_updatedFloor);

                return Ok(_updatedFloor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Floor Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Floor ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /floor/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the floor ID is null</response>
        /// <response code="404">If the floor ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteFloor(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _floorEntity = await _repositoryWrapper.Floor.GetFloorById(id);

                if (_floorEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Floor.DeleteFloor(_floorEntity);
                await _repositoryWrapper.Save();


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        #endregion

        #region Private Methods

        private async Task<GetFloorDto> AddNamesToDtoId(GetFloorDto floorMap)
        {
            if (floorMap.BuildingId != null)
            {
                var building = await _repositoryWrapper.Building.GetBuildingById((int)floorMap.BuildingId);
                floorMap.BuildingName = building.Name;
            }

            return floorMap;
        }

        private async Task<IEnumerable<GetFloorDto>> AddNamesToDtoIdArray(IEnumerable<GetFloorDto> floorsMap)
        {
            var buildings = await _repositoryWrapper.Building.GetAllBuildings();

            foreach (var floor in floorsMap)
            {
                if (floor.BuildingId != null)
                {
                    floor.BuildingName = buildings.FirstOrDefault(b => b.Id == floor.BuildingId).Name;
                }
            }

            return floorsMap;
        }

        #endregion
    }
}
