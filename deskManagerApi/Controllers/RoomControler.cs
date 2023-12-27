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
    public class RoomController : ControllerBase
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
        /// The Constructor of Room Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public RoomController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Rooms.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /room
        ///
        /// </remarks>
        /// <response code="200">If Room ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetRoomDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                var _rooms = await _repositoryWrapper.Room.GetAllRooms();

                var _roomsMap = _mapper.Map<IEnumerable<GetRoomDto>>(_rooms);

                return Ok(_roomsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Room for provided ID.
        /// </summary>
        /// <param name="id">The Room ID value to get the Room object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /room/1
        ///
        /// </remarks>
        /// <response code="200">If Room ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetRoomById")]
        [ProducesResponseType((200), Type = typeof(GetRoomDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRoomById(int id)
        {
            try
            {
                var _room = await _repositoryWrapper.Room.GetRoomById(id);

                if( _room == null )
                {
                    return NotFound();
                }

                var _roomMap = _mapper.Map<GetRoomDto>(_room);

                return Ok(_roomMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a room with the given parameters.
        /// </summary>
        /// <param name="room">Value for create Room model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /room
        ///     
        ///     {
        ///        "name": "Room #1",
        ///        "floorId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the room is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetRoomDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRoom([FromBody]CreateRoomDto room)
        {
            try
            {
                if (room is null)
                {
                    return BadRequest("Room object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if(room.FloorId != null)
                {
                    var _floorEntity = await _repositoryWrapper.Floor.GetFloorById((int)room.FloorId);

                    if(_floorEntity is null)
                    {
                        return BadRequest("Invalid floor ID");
                    }
                }

                var _roomEntity = _mapper.Map<Room>(room);
                await _repositoryWrapper.Room.CreateRoom(_roomEntity);
                await  _repositoryWrapper.Save();

                var _createdRoom = _mapper.Map<GetRoomDto>(_roomEntity);

                return CreatedAtRoute("GetRoomById", new { id = _createdRoom.Id }, _createdRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Room with provided model.
        /// </summary>
        /// <param name="room">Value for update Room model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /room
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Room #1",
        ///        "floorId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the room is null or invalid</response>
        /// <response code="404">If the room is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetRoomDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomDto room)
        {
            try
            {
                if (room is null)
                {
                    return BadRequest("Room object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _roomEntity = await _repositoryWrapper.Room.GetRoomById(room.Id);

                if(_roomEntity is null)
                {
                    return NotFound();
                }

                if (room.FloorId != null)
                {
                    var _floorEntity = await _repositoryWrapper.Floor.GetFloorById((int)room.FloorId);

                    if (_floorEntity is null)
                    {
                        return BadRequest("Invalid floor ID");
                    }
                }

                _mapper.Map(room, _roomEntity);
                _repositoryWrapper.Room.UpdateRoom(_roomEntity);
                await _repositoryWrapper.Save();

                var _updatedRoom = _mapper.Map<GetRoomDto>(_roomEntity);

                return Ok(_updatedRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Room Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Room ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /room/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the room ID is null</response>
        /// <response code="404">If the room ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _roomEntity = await _repositoryWrapper.Room.GetRoomById(id);

                if (_roomEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Room.DeleteRoom(_roomEntity);
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
