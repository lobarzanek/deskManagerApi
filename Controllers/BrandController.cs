#region Usings

using AutoMapper;
using deskManagerApi.Contracts;
using deskManagerApi.DTO.Create;
using deskManagerApi.DTO.Get;
using deskManagerApi.DTO.Update;
using deskManagerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace deskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
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
        /// The Constructor of Brand Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public BrandController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Brands.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /brand
        ///
        /// </remarks>
        /// <response code="200">If Brand ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetBrandDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var _brands = await _repositoryWrapper.Brand.GetAllBrands();

                var _brandsMap = _mapper.Map<IEnumerable<GetBrandDto>>(_brands);

                return Ok(_brandsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Brand for provided ID.
        /// </summary>
        /// <param name="id">The Brand ID value to get the Brand object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /brand/1
        ///
        /// </remarks>
        /// <response code="200">If Brand ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetBrandById")]
        [ProducesResponseType((200), Type = typeof(GetBrandDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBrandById(int id)
        {
            try
            {
                var _brand = await _repositoryWrapper.Brand.GetBrandById(id);

                if( _brand == null )
                {
                    return NotFound();
                }

                var _brandMap = _mapper.Map<GetBrandDto>(_brand);

                return Ok(_brandMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a brand with the given parameters.
        /// </summary>
        /// <param name="brand">Value for create Brand model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /brand
        ///     
        ///     {
        ///        "name": "Brand #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the brand is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetBrandDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBrand([FromBody]CreateBrandDto brand)
        {
            try
            {
                if (brand is null)
                {
                    return BadRequest("Brand object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _brandEntity = _mapper.Map<Brand>(brand);

                await _repositoryWrapper.Brand.CreateBrand(_brandEntity);
                await  _repositoryWrapper.Save();

                var _createdBrand = _mapper.Map<GetBrandDto>(_brandEntity);

                return CreatedAtRoute("GetBrandById", new { id = _createdBrand.Id }, _createdBrand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Brand with provided model.
        /// </summary>
        /// <param name="brand">Value for update Brand model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /brand
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Brand #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the brand is null or invalid</response>
        /// <response code="404">If the brand is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetBrandDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandDto brand)
        {
            try
            {
                if (brand is null)
                {
                    return BadRequest("Brand object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _brandEntity = await _repositoryWrapper.Brand.GetBrandById(brand.Id);

                if(_brandEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(brand, _brandEntity);
                _repositoryWrapper.Brand.UpdateBrand(_brandEntity);
                await _repositoryWrapper.Save();

                var _updatedBrand = _mapper.Map<GetBrandDto>(_brandEntity);

                return Ok(_updatedBrand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Brand Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Brand ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /brand/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the brand ID is null</response>
        /// <response code="404">If the brand ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _brandEntity = await _repositoryWrapper.Brand.GetBrandById(id);

                if (_brandEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Brand.DeleteBrand(_brandEntity);
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
