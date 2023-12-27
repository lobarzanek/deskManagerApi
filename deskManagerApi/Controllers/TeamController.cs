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
    public class TeamController : ControllerBase
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
        /// The Constructor of Team Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public TeamController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Teams.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /team
        ///
        /// </remarks>
        /// <response code="200">If Team ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetTeamDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllTeams()
        {
            try
            {
                var _teams = await _repositoryWrapper.Team.GetAllTeams();

                var _teamsMap = _mapper.Map<IEnumerable<GetTeamDto>>(_teams);

                return Ok(_teamsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Team for provided ID.
        /// </summary>
        /// <param name="id">The Team ID value to get the Team object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /team/1
        ///
        /// </remarks>
        /// <response code="200">If Team ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetTeamById")]
        [ProducesResponseType((200), Type = typeof(GetTeamDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTeamById(int id)
        {
            try
            {
                var _team = await _repositoryWrapper.Team.GetTeamById(id);

                if( _team == null )
                {
                    return NotFound();
                }

                var _teamMap = _mapper.Map<GetTeamDto>(_team);

                return Ok(_teamMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a team with the given parameters.
        /// </summary>
        /// <param name="team">Value for create Team model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /team
        ///     
        ///     {
        ///        "name": "Team #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the team is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetTeamDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateTeam([FromBody]CreateTeamDto team)
        {
            try
            {
                if (team is null)
                {
                    return BadRequest("Team object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _teamEntity = _mapper.Map<Team>(team);

                await _repositoryWrapper.Team.CreateTeam(_teamEntity);
                await  _repositoryWrapper.Save();

                var _createdTeam = _mapper.Map<GetTeamDto>(_teamEntity);

                return CreatedAtRoute("GetTeamById", new { id = _createdTeam.Id }, _createdTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Team with provided model.
        /// </summary>
        /// <param name="team">Value for update Team model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /team
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Team #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the team is null or invalid</response>
        /// <response code="404">If the team is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetTeamDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto team)
        {
            try
            {
                if (team is null)
                {
                    return BadRequest("Team object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _teamEntity = await _repositoryWrapper.Team.GetTeamById(team.Id);

                if(_teamEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(team, _teamEntity);
                _repositoryWrapper.Team.UpdateTeam(_teamEntity);
                await _repositoryWrapper.Save();

                var _updatedTeam = _mapper.Map<GetTeamDto>(_teamEntity);

                return Ok(_updatedTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Team Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Team ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /team/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the team ID is null</response>
        /// <response code="404">If the team ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _teamEntity = await _repositoryWrapper.Team.GetTeamById(id);

                if (_teamEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Team.DeleteTeam(_teamEntity);
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
