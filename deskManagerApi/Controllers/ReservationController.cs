using AutoMapper;
using deskManagerApi.Contracts;
using deskManagerApi.Entities.DTO.Create;
using deskManagerApi.Entities.DTO.Get;
using deskManagerApi.Entities.DTO.Update;
using deskManagerApi.Entities.Models;
using deskManagerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace deskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
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
        /// The Constructor of Reservation Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public ReservationController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Reservations.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservation
        ///
        /// </remarks>
        /// <response code="200">If Reservation ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetReservationDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var _reservations = await _repositoryWrapper.Reservation.GetAllReservations();

                if(_reservations == null)
                {
                    return Ok();
                }

                var _reservationsMap = await AddInfoToReservations(_reservations);

                return Ok(_reservationsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Reservation for provided ID.
        /// </summary>
        /// <param name="id">The User ID value to get the Reservations</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservation/1
        ///
        /// </remarks>
        /// <response code="200">If Reservation ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetReservationById")]
        [ProducesResponseType((200), Type = typeof(GetReservationDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetReservationById(int id)
        {
            try
            {
                var _reservation = await _repositoryWrapper.Reservation.GetReservationById(id);

                var _reservationMap = await AddInfoToReservation(_reservation);

                return Ok(_reservationMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Reservation for provided user ID.
        /// </summary>
        /// <param name="id">The Reservation ID value to get the Reservation object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /reservation/user/1
        ///
        /// </remarks>
        /// <response code="200">If User ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("user/{id}", Name = "GetReservationsByUserId")]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetReservationDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetReservationsByUserId(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var user = await _repositoryWrapper.User.GetUserById(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var _reservations = await _repositoryWrapper.Reservation.GetReservationsByUserId(id);

                if (_reservations == null)
                {
                    return Ok();
                }

                var _reservationsMap = await AddInfoToReservations(_reservations);

                return Ok(_reservationsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a reservation with the given parameters.
        /// </summary>
        /// <param name="reservation">Value for create Reservation model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /reservation
        ///     
        ///     {
        ///        "date": "2024-01-01T00:00:00.000Z",
        ///        "userId": 1,
        ///        "deskId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the reservation is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetReservationDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto reservation)
        {
            try
            {
                if (reservation is null)
                {
                    return BadRequest("Reservation object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var existingReservations = await _repositoryWrapper.Reservation.GetAllReservations();

                if (existingReservations.Any(r => DateOnly.FromDateTime(r.Date) == DateOnly.FromDateTime(reservation.Date)))
                {
                    return BadRequest("Desk is claimed for provided Date");
                }

                var _reservationEntity = _mapper.Map<Reservation>(reservation);

                await _repositoryWrapper.Reservation.CreateReservation(_reservationEntity);
                await _repositoryWrapper.Save();

                var _createdReservation = await AddInfoToReservation(_reservationEntity);

                return CreatedAtRoute("GetReservationById", new { id = _createdReservation.Id }, _createdReservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }


        /// <summary>
        /// Delete Reservation Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Reservation ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /reservation/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the reservation ID is null</response>
        /// <response code="404">If the reservation ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _reservationEntity = await _repositoryWrapper.Reservation.GetReservationById(id);

                if (_reservationEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Reservation.DeleteReservation(_reservationEntity);
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

        private async Task<GetReservationDto> AddInfoToReservation(Reservation reservation)
        {
            var reservationMap = new GetReservationDto() { Id = reservation.Id, Date = reservation.Date };

            if (reservation.DeskId != null)
            {
                var desk = await _repositoryWrapper.Desk.GetDeskById((int)reservation.DeskId);

                if (desk != null)
                {
                    reservationMap.DeskName = desk.Name;

                    if (desk.RoomId != null)
                    {
                        var room = await _repositoryWrapper.Room.GetRoomById((int)desk.RoomId);
                        if (room != null)
                        {
                            reservationMap.RoomName = room.Name;

                            if (room.FloorId != null)
                            {
                                var floor = await _repositoryWrapper.Floor.GetFloorById((int)room.FloorId);
                                if (floor != null)
                                {
                                    reservationMap.FloorName = floor.Name;

                                    if (floor.BuildingId != null)
                                    {
                                        var building = await _repositoryWrapper.Building.GetBuildingById((int)floor.BuildingId);
                                        if (building != null)
                                        {
                                            reservationMap.BuildingName = building.Name;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (reservation.UserId != null)
            {
                var user = await _repositoryWrapper.User.GetUserById((int)reservation.UserId);

                if (user != null)
                {
                    reservationMap.UserName = $"{user.FirstName} {user.LastName}";
                }
            }

            return reservationMap;
        }

        private async Task<IEnumerable<GetReservationDto>> AddInfoToReservations(IEnumerable<Reservation> reservations)
        {
            var desks = await _repositoryWrapper.Desk.GetAllDesks();
            var rooms = await _repositoryWrapper.Room.GetAllRooms();
            var floors = await _repositoryWrapper.Floor.GetAllFloors();
            var buildings = await _repositoryWrapper.Building.GetAllBuildings();
            var users = await _repositoryWrapper.User.GetAllUsers();

            var reservationsMap = new List<GetReservationDto>();

            foreach (var reservation in reservations)
            {
                var reservationMap = new GetReservationDto() {Id = reservation.Id, Date = reservation.Date };

                if (reservation.DeskId != null)
                {
                    var desk = desks.FirstOrDefault(d => d.Id == reservation.DeskId);
                    if (desk != null)
                    {
                        reservationMap.DeskName = desk.Name;

                        if (desk.RoomId != null)
                        {
                            var room = rooms.FirstOrDefault(r => r.Id == desk.RoomId);

                            if (room != null)
                            {
                                reservationMap.RoomName = room.Name;

                                if (room.FloorId != null)
                                {
                                    var floor = floors.FirstOrDefault(f => f.Id == room.FloorId);

                                    if (floor != null)
                                    {
                                        reservationMap.FloorName = floor.Name;

                                        if (floor.BuildingId != null)
                                        {
                                            var building = buildings.FirstOrDefault(b => b.Id == floor.BuildingId);
                                            if (building != null)
                                            {
                                                reservationMap.BuildingName = building.Name;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (reservation.UserId != null)
                {
                    var user = users.FirstOrDefault(u => u.Id == reservation.UserId);
                    if (user != null)
                    {
                        reservationMap.UserName = $"{user.FirstName} {user.LastName}";
                    }
                }
                reservationsMap.Add(reservationMap);
            }

            return reservationsMap;
        }

        #endregion
       
    }
}
