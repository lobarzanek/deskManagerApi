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
    public class UserController : ControllerBase
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
        /// The Constructor of User Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public UserController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Users.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /user
        ///
        /// </remarks>
        /// <response code="200">If User ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetUserDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var _users = await _repositoryWrapper.User.GetAllUsers();

                var _usersMap = _mapper.Map<IEnumerable<GetUserDto>>(_users);

                return Ok(_usersMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a User for provided ID.
        /// </summary>
        /// <param name="id">The User ID value to get the User object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /user/1
        ///
        /// </remarks>
        /// <response code="200">If User ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType((200), Type = typeof(GetUserDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var _user = await _repositoryWrapper.User.GetUserById(id);

                if (_user == null)
                {
                    return NotFound();
                }

                var _userMap = _mapper.Map<GetUserDto>(_user);

                return Ok(_userMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a user with the given parameters.
        /// </summary>
        /// <param name="user">Value for create User model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /user
        ///     
        ///     {
        ///        "firstName": "John",
        ///        "lastName": "Doe",
        ///        "login": "jonh_d",
        ///        "password": "johnpass",
        ///        "role": 1,
        ///        "team": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the user is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                if (user is null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (user.TeamId != null)
                {
                    var _team = _repositoryWrapper.Team.GetTeamById((int)user.TeamId);
                    if (_team is null)
                    {
                        return BadRequest("Invalid team ID");
                    }
                }

                var _userEntity = _mapper.Map<User>(user);

                await _repositoryWrapper.User.CreateUser(_userEntity);
                await _repositoryWrapper.Save();

                var _createdUser = _mapper.Map<GetUserDto>(_userEntity);

                return CreatedAtRoute("GetUserById", new { id = _createdUser.Id }, _createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates User with provided model.
        /// </summary>
        /// <param name="user">Value for update User model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /user
        ///     
        ///     {
        ///        "id": 1,
        ///        "firstName": "John",
        ///        "lastName": "Doe",
        ///        "login": "jonh_d",
        ///        "role": 1,
        ///        "team": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the user is null or invalid</response>
        /// <response code="404">If the user is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetUserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            try
            {
                if (user is null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (user.TeamId != null)
                {
                    var _team = _repositoryWrapper.Team.GetTeamById((int)user.TeamId);
                    if (_team is null)
                    {
                        return BadRequest("Invalid team ID");
                    }
                }

                var _userEntity = await _repositoryWrapper.User.GetUserById(user.Id);

                if (_userEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(user, _userEntity);
                _repositoryWrapper.User.UpdateUser(_userEntity);
                await _repositoryWrapper.Save();

                var _updatedUser = _mapper.Map<GetUserDto>(_userEntity);

                return Ok(_updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete User Model by provided ID.
        /// </summary>
        /// <param name="id">Value for User ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /user/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the user ID is null</response>
        /// <response code="404">If the user ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _userEntity = await _repositoryWrapper.User.GetUserById(id);

                if (_userEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.User.DeleteUser(_userEntity);
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
