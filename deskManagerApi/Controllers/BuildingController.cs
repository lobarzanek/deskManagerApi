using AutoMapper;
using deskManagerApi.Contracts;
using deskManagerApi.Entities.DTO.Create;
using deskManagerApi.Entities.DTO.Get;
using deskManagerApi.Entities.DTO.Update;
using deskManagerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
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
        /// The Constructor of Building Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public BuildingController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all buildings.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /building
        ///
        /// </remarks>
        /// <response code="200">If Building ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetBuildingDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllBuildings()
        {
            try
            {
                var _buildings = await _repositoryWrapper.Building.GetAllBuildings();

                var _builidingsMap = _mapper.Map<IEnumerable<GetBuildingDto>>(_buildings);

                return Ok(_builidingsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Building for provided ID.
        /// </summary>
        /// <param name="id">The Building ID value to get the building object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /building/1
        ///
        /// </remarks>
        /// <response code="200">If building ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetBuildingById")]
        [ProducesResponseType((200), Type = typeof(GetBuildingDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBuildingById(int id)
        {
            try
            {
                var _building = await _repositoryWrapper.Building.GetBuildingById(id);

                if (_building == null)
                {
                    return NotFound();
                }

                var __buildingMap = _mapper.Map<GetBuildingDto>(_building);

                return Ok(__buildingMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a building with the given parameters.
        /// </summary>
        /// <param name="building">Value for create building model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /building
        ///     
        ///     {
        ///        "name": "Building #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the building is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetBuildingDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBuilding([FromBody] CreateBuildingDto building)
        {
            try
            {
                if (building is null)
                {
                    return BadRequest("Building object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _buildingEntity = _mapper.Map<Building>(building);

                await _repositoryWrapper.Building.CreateBuilding(_buildingEntity);
                await _repositoryWrapper.Save();

                var _createdBuilding = _mapper.Map<GetBuildingDto>(_buildingEntity);

                return CreatedAtRoute("GetBuildingById", new { id = _createdBuilding.Id }, _createdBuilding);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates building with provided model.
        /// </summary>
        /// <param name="building">Value for update building model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /building
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Building #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the building is null or invalid</response>
        /// <response code="404">If the building is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetBuildingDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBuilding([FromBody] UpdateBuildingDto building)
        {
            try
            {
                if (building is null)
                {
                    return BadRequest("Building object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _buildingEntity = await _repositoryWrapper.Building.GetBuildingById(building.Id);

                if (_buildingEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(building, _buildingEntity);
                _repositoryWrapper.Building.UpdateBuilding(_buildingEntity);
                await _repositoryWrapper.Save();

                var _updatedBuilding = _mapper.Map<GetBuildingDto>(_buildingEntity);

                return Ok(_updatedBuilding);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete building Model by provided ID.
        /// </summary>
        /// <param name="id">Value for building ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /building/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the building ID is null</response>
        /// <response code="404">If the building ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _buildingEntity = await _repositoryWrapper.Building.GetBuildingById(id);

                if (_buildingEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Building.DeleteBuilding(_buildingEntity);
                await _repositoryWrapper.Save();


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        #endregion
    }
}
