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
    public class DeskController : ControllerBase
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
        /// The Constructor of Desk Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public DeskController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a list of all Desks.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /desk
        ///
        /// </remarks>
        /// <response code="200">If Desk ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet(Name = "GetAllDesks")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetDeskDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllDesks()
        {
            try
            {
                var _desks = await _repositoryWrapper.Desk.GetAllDesks();

                var _desksMap = _mapper.Map<IEnumerable<GetDeskDto>>(_desks);

                _desksMap = await AddNamesToDtoIdArray(_desksMap);

                return Ok(_desksMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a list of all Desks with basic information.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /desk/basic
        ///
        /// </remarks>
        /// <response code="200">If Desk ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("basic", Name = "GetAllDesksBasicInfo")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetDeskBasicInfo>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllDesksBasicInfo()
        {
            try
            {
                var _desks = await _repositoryWrapper.Desk.GetAllDesks();

                var _desksMap = _mapper.Map<IEnumerable<GetDeskBasicInfo>>(_desks);

                return Ok(_desksMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Desk for provided ID.
        /// </summary>
        /// <param name="id">The Desk ID value to get the Desk object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /desk/1
        ///
        /// </remarks>
        /// <response code="200">If Desk ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetDeskById")]
        [ProducesResponseType((200), Type = typeof(GetDeskDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDeskById(int id)
        {
            try
            {
                var _desk = await _repositoryWrapper.Desk.GetDeskById(id);

                if (_desk == null)
                {
                    return NotFound();
                }

                var _deskMap = _mapper.Map<GetDeskDto>(_desk);

                _deskMap = await AddNamesToDtoId(_deskMap);

                return Ok(_deskMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a desk with the given parameters.
        /// </summary>
        /// <param name="desk">Value for create Desk model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /desk
        ///     
        ///     {
        ///        "name": "Desk #1",
        ///        "mapXLocation": "10",
        ///        "mapYLocation": "10",
        ///        "width": "80",
        ///        "height": "30",
        ///        "roomId": 1,
        ///        "status": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the desk is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetDeskDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateDesk([FromBody] CreateDeskDto desk)
        {
            try
            {
                if (desk is null)
                {
                    return BadRequest("Desk object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (desk.RoomId != null)
                {
                    var _room = _repositoryWrapper.Room.GetRoomById((int)desk.RoomId);
                    if (_room is null)
                    {
                        return BadRequest("Invalid room ID");
                    }
                }

                if (desk.StatusId != null)
                {
                    var _deskStatus = _repositoryWrapper.DeskStatus.GetDeskStatusById((int)desk.StatusId);
                    if (_deskStatus is null)
                    {
                        return BadRequest("Invalid status ID");
                    }
                }

                var _deskEntity = _mapper.Map<Desk>(desk);

                await _repositoryWrapper.Desk.CreateDesk(_deskEntity);
                await _repositoryWrapper.Save();

                var _createdDesk = _mapper.Map<GetDeskDto>(_deskEntity);

                _createdDesk = await AddNamesToDtoId(_createdDesk);

                return CreatedAtRoute("GetDeskById", new { id = _createdDesk.Id }, _createdDesk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Desk with provided model.
        /// </summary>
        /// <param name="desk">Value for update Desk model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /desk
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Desk #1",
        ///        "mapXLocation": "10",
        ///        "mapYLocation": "10",
        ///        "width": "80",
        ///        "height": "30",
        ///        "roomId": 1,
        ///        "statusId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the desk is null or invalid</response>
        /// <response code="404">If the desk is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetDeskDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateDesk([FromBody] UpdateDeskDto desk)
        {
            try
            {
                if (desk is null)
                {
                    return BadRequest("Desk object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (desk.RoomId != null)
                {
                    var _desk = await _repositoryWrapper.Room.GetRoomById((int)desk.RoomId);
                    if (_desk is null)
                    {
                        return BadRequest("Invalid desk ID");
                    }
                }

                if (desk.StatusId != null)
                {
                    var _deskStatus = await _repositoryWrapper.DeskStatus.GetDeskStatusById((int)desk.StatusId);
                    if (_deskStatus is null)
                    {
                        return BadRequest("Invalid status ID");
                    }
                }

                var _deskEntity = await _repositoryWrapper.Desk.GetDeskById(desk.Id);

                if (_deskEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(desk, _deskEntity);
                _repositoryWrapper.Desk.UpdateDesk(_deskEntity);
                await _repositoryWrapper.Save();

                var _updatedDesk = _mapper.Map<GetDeskDto>(_deskEntity);

                _updatedDesk = await AddNamesToDtoId(_updatedDesk);

                return Ok(_updatedDesk);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Desk Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Desk ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /desk/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the desk ID is null</response>
        /// <response code="404">If the desk ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteDesk(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _deskEntity = await _repositoryWrapper.Desk.GetDeskById(id);

                if (_deskEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Desk.DeleteDesk(_deskEntity);
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

        private async Task<GetDeskDto> AddNamesToDtoId(GetDeskDto deskMap)
        {
            if (deskMap.RoomId != null)
            {
                var room = await _repositoryWrapper.Room.GetRoomById((int)deskMap.RoomId);
                deskMap.RoomName = room.Name;
            }

            if (deskMap.StatusId != null)
            {
                var status = await _repositoryWrapper.DeskStatus.GetDeskStatusById((int)deskMap.StatusId);
                deskMap.StatusName = status.Name;
            }

            return deskMap;
        }

        private async Task<IEnumerable<GetDeskDto>> AddNamesToDtoIdArray(IEnumerable<GetDeskDto> desksMap)
        {
            var rooms = await _repositoryWrapper.Room.GetAllRooms();
            var statuses = await _repositoryWrapper.DeskStatus.GetAllDeskStatuses();

            foreach(var desk in desksMap)
            {
                if (desk.RoomId != null)
                {
                    desk.RoomName = rooms.FirstOrDefault(r => r.Id == desk.RoomId).Name;
                }

                if (desk.StatusId != null)
                {
                    desk.StatusName = statuses.FirstOrDefault(s => s.Id == desk.StatusId).Name;
                }
            }

            return desksMap;
        }

        #endregion
    }
}
