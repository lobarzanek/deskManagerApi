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
    public class DeskStatusController : ControllerBase
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
        /// The Constructor of DeskStatus Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public DeskStatusController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all DeskStatuses.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /deskStatus
        ///
        /// </remarks>
        /// <response code="200">If DeskStatus ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetDeskStatusDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllDeskStatuss()
        {
            try
            {
                var _deskStatuss = await _repositoryWrapper.DeskStatus.GetAllDeskStatuses();

                var _deskStatussMap = _mapper.Map<IEnumerable<GetDeskStatusDto>>(_deskStatuss);

                return Ok(_deskStatussMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a DeskStatus for provided ID.
        /// </summary>
        /// <param name="id">The DeskStatus ID value to get the DeskStatus object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /deskStatus/1
        ///
        /// </remarks>
        /// <response code="200">If DeskStatus ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetDeskStatusById")]
        [ProducesResponseType((200), Type = typeof(GetDeskStatusDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDeskStatusById(int id)
        {
            try
            {
                var _deskStatus = await _repositoryWrapper.DeskStatus.GetDeskStatusById(id);

                if (_deskStatus == null)
                {
                    return NotFound();
                }

                var _deskStatusMap = _mapper.Map<GetDeskStatusDto>(_deskStatus);

                return Ok(_deskStatusMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a deskStatus with the given parameters.
        /// </summary>
        /// <param name="deskStatus">Value for create DeskStatus model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /deskStatus
        ///     
        ///     {
        ///        "name": "DeskStatus #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the deskStatus is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetDeskStatusDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateDeskStatus([FromBody] CreateDeskStatusDto deskStatus)
        {
            try
            {
                if (deskStatus is null)
                {
                    return BadRequest("DeskStatus object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _deskStatusEntity = _mapper.Map<DeskStatus>(deskStatus);

                await _repositoryWrapper.DeskStatus.CreateDeskStatus(_deskStatusEntity);
                await _repositoryWrapper.Save();

                var _createdDeskStatus = _mapper.Map<GetDeskStatusDto>(_deskStatusEntity);

                return CreatedAtRoute("GetDeskStatusById", new { id = _createdDeskStatus.Id }, _createdDeskStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates DeskStatus with provided model.
        /// </summary>
        /// <param name="deskStatus">Value for update DeskStatus model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /deskStatus
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "DeskStatus #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the deskStatus is null or invalid</response>
        /// <response code="404">If the deskStatus is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetDeskStatusDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateDeskStatus([FromBody] UpdateDeskStatusDto deskStatus)
        {
            try
            {
                if (deskStatus is null)
                {
                    return BadRequest("DeskStatus object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _deskStatusEntity = await _repositoryWrapper.DeskStatus.GetDeskStatusById(deskStatus.Id);

                if (_deskStatusEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(deskStatus, _deskStatusEntity);
                _repositoryWrapper.DeskStatus.UpdateDeskStatus(_deskStatusEntity);
                await _repositoryWrapper.Save();

                var _updatedDeskStatus = _mapper.Map<GetDeskStatusDto>(_deskStatusEntity);

                return Ok(_updatedDeskStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete DeskStatus Model by provided ID.
        /// </summary>
        /// <param name="id">Value for DeskStatus ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /deskStatus/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the deskStatus ID is null</response>
        /// <response code="404">If the deskStatus ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteDeskStatus(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _deskStatusEntity = await _repositoryWrapper.DeskStatus.GetDeskStatusById(id);

                if (_deskStatusEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.DeskStatus.DeleteDeskStatus(_deskStatusEntity);
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
