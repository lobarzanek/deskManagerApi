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
    public class IssueController : ControllerBase
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
        /// The Constructor of Issue Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public IssueController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Issues.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /issue
        ///
        /// </remarks>
        /// <response code="200">If Issue ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetIssueDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllIssues()
        {
            try
            {
                var _issues = await _repositoryWrapper.Issue.GetAllIssues();

                var _issuesMap = _mapper.Map<IEnumerable<GetIssueDto>>(_issues);

                return Ok(_issuesMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Issue for provided ID.
        /// </summary>
        /// <param name="id">The Issue ID value to get the Issue object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /issue/1
        ///
        /// </remarks>
        /// <response code="200">If Issue ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetIssueById")]
        [ProducesResponseType((200), Type = typeof(GetIssueDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetIssueById(int id)
        {
            try
            {
                var _issue = await _repositoryWrapper.Issue.GetIssueById(id);

                if (_issue == null)
                {
                    return NotFound();
                }

                var _issueMap = _mapper.Map<GetIssueDto>(_issue);

                return Ok(_issueMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a issue with the given parameters.
        /// </summary>
        /// <param name="issue">Value for create Issue model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /issue
        ///     
        ///     {
        ///        "description": "Issue description"
        ///        "status": 1,
        ///        "deskId": 1,
        ///        "reporterId: 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the issue is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetIssueDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto issue)
        {
            try
            {
                if (issue is null)
                {
                    return BadRequest("Issue object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _user = _repositoryWrapper.User.GetUserById(issue.ReporterId);

                if (_user == null)
                {
                    return BadRequest("Invalid reporter ID");
                }

                var _desk = _repositoryWrapper.Desk.GetDeskById(issue.DeskId);

                if (_desk == null)
                {
                    return BadRequest("Invalid desk ID");
                }

                var _issueEntity = _mapper.Map<Issue>(issue);

                await _repositoryWrapper.Issue.CreateIssue(_issueEntity);
                await _repositoryWrapper.Save();

                var _createdIssue = _mapper.Map<GetIssueDto>(_issueEntity);

                return CreatedAtRoute("GetIssueById", new { id = _createdIssue.Id }, _createdIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Issue with provided model.
        /// </summary>
        /// <param name="issue">Value for update Issue model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /issue
        ///     
        ///     {
        ///        "id": 1,
        ///        "description": "Issue description"
        ///        "status": 1,
        ///        "deskId": 1,
        ///        "reporterId: 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the issue is null or invalid</response>
        /// <response code="404">If the issue is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetIssueDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateIssue([FromBody] UpdateIssueDto issue)
        {
            try
            {
                if (issue is null)
                {
                    return BadRequest("Issue object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _user = _repositoryWrapper.User.GetUserById(issue.ReporterId);

                if (_user == null)
                {
                    return BadRequest("Invalid reporter ID");
                }

                var _desk = _repositoryWrapper.Desk.GetDeskById(issue.DeskId);

                if (_desk == null)
                {
                    return BadRequest("Invalid desk ID");
                }

                var _issueEntity = await _repositoryWrapper.Issue.GetIssueById(issue.Id);

                if (_issueEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(issue, _issueEntity);
                _repositoryWrapper.Issue.UpdateIssue(_issueEntity);
                await _repositoryWrapper.Save();

                var _updatedIssue = _mapper.Map<GetIssueDto>(_issueEntity);

                return Ok(_updatedIssue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Issue Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Issue ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /issue/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the issue ID is null</response>
        /// <response code="404">If the issue ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _issueEntity = await _repositoryWrapper.Issue.GetIssueById(id);

                if (_issueEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Issue.DeleteIssue(_issueEntity);
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
