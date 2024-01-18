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
    public class ItemController : ControllerBase
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
        /// The Constructor of Item Controller
        /// </summary>
        /// <param name="repositoryWrapper">Value for repository wrapper interface</param>
        /// <param name="mapper">Value for AutoMapper interface</param>
        public ItemController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Items.
        /// </summary>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /item
        ///
        /// </remarks>
        /// <response code="200">If Item ID is valid</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(IEnumerable<GetItemDto>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var _items = await _repositoryWrapper.Item.GetAllItems();

                var _itemsMap = _mapper.Map<IEnumerable<GetItemDto>>(_items);

                _itemsMap = await AddNamesToDtoIdArray(_itemsMap);

                return Ok(_itemsMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Returns a Item for provided ID.
        /// </summary>
        /// <param name="id">The Item ID value to get the Item object</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /item/1
        ///
        /// </remarks>
        /// <response code="200">If Item ID is valid</response>
        /// <response code="404">If the ID is not found in database</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet("{id}", Name = "GetItemById")]
        [ProducesResponseType((200), Type = typeof(GetItemDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var _item = await _repositoryWrapper.Item.GetItemById(id);

                if (_item == null)
                {
                    return NotFound();
                }

                var _itemMap = _mapper.Map<GetItemDto>(_item);

                _itemMap = await AddNamesToDtoId(_itemMap);

                return Ok(_itemMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Creates a item with the given parameters.
        /// </summary>
        /// <param name="item">Value for create Item model.</param>
        /// <returns>201 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /item
        ///     
        ///     {
        ///        "name": "Mouse #1",
        ///        "serialNumber": "ABC123",
        ///        "status": 2,
        ///        "type": 1,
        ///        "ownerId": 1,
        ///        "brandId": 1,
        ///        "deskId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">If the creation was successful.</response>
        /// <response code="400">If the item is null or invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost]
        [ProducesResponseType((201), Type = typeof(GetItemDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto item)
        {
            try
            {
                if (item is null)
                {
                    return BadRequest("Item object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if(item.OwnerId != null)
                {
                    var _owner = await _repositoryWrapper.User.GetUserById((int)item.OwnerId);
                    if (_owner is null)
                    {
                        return BadRequest("Invalid owner ID");
                    }
                }

                if (item.BrandId != null)
                {
                    var _brand = await _repositoryWrapper.Brand.GetBrandById((int)item.BrandId);
                    if (_brand is null)
                    {
                        return BadRequest("Invalid brand ID");
                    }
                }

                if (item.DeskId != null)
                {
                    var _desk = await _repositoryWrapper.Desk.GetDeskById((int)item.DeskId);
                    if (_desk is null)
                    {
                        return BadRequest("Invalid desk ID");
                    }

                    item.Status = ItemStatus.Used;
                }
                else if(item.DeskId == null && item.Status != ItemStatus.Broken)
                {
                    item.Status = ItemStatus.Free;
                }

                var _itemEntity = _mapper.Map<Item>(item);

                await _repositoryWrapper.Item.CreateItem(_itemEntity);
                await _repositoryWrapper.Save();

                var _createdItem = _mapper.Map<GetItemDto>(_itemEntity);

                _createdItem = await AddNamesToDtoId(_createdItem);

                return CreatedAtRoute("GetItemById", new { id = _createdItem.Id }, _createdItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Updates Item with provided model.
        /// </summary>
        /// <param name="item">Value for update Item model.</param>
        /// <returns>200 Status Code for success.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /item
        ///     
        ///     {
        ///        "id": 1,
        ///        "name": "Mouse #1",
        ///        "serialNumber": "ABC123",
        ///        "status": 2,
        ///        "type": 1,
        ///        "ownerId": 1,
        ///        "brandId": 1,
        ///        "deskId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If the item is null or invalid</response>
        /// <response code="404">If the item is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(GetItemDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemDto item)
        {
            try
            {
                if (item is null)
                {
                    return BadRequest("Item object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (item.OwnerId != null)
                {
                    var _owner = await _repositoryWrapper.User.GetUserById((int)item.OwnerId);
                    if (_owner is null)
                    {
                        return BadRequest("Invalid owner ID");
                    }
                }

                if (item.BrandId != null)
                {
                    var _brand = await _repositoryWrapper.Brand.GetBrandById((int)item.BrandId);
                    if (_brand is null)
                    {
                        return BadRequest("Invalid brand ID");
                    }
                }

                if (item.DeskId != null)
                {
                    var _desk = await _repositoryWrapper.Desk.GetDeskById((int)item.DeskId);
                    if (_desk is null)
                    {
                        return BadRequest("Invalid desk ID");
                    }

                    item.Status = ItemStatus.Used;
                }
                else if (item.DeskId == null && item.Status != ItemStatus.Broken)
                {
                    item.Status = ItemStatus.Free;
                }

                var _itemEntity = await _repositoryWrapper.Item.GetItemById(item.Id);

                if (_itemEntity is null)
                {
                    return NotFound();
                }

                _mapper.Map(item, _itemEntity);
                _repositoryWrapper.Item.UpdateItem(_itemEntity);
                await _repositoryWrapper.Save();

                var _updatedItem = _mapper.Map<GetItemDto>(_itemEntity);

                _updatedItem = await AddNamesToDtoId(_updatedItem);

                return Ok(_updatedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Delete Item Model by provided ID.
        /// </summary>
        /// <param name="id">Value for Item ID</param>
        /// <returns>204 Status Code for success</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /item/1
        ///
        /// </remarks>
        /// <response code="204">If delete was successful</response>
        /// <response code="400">If the item ID is null</response>
        /// <response code="404">If the item ID is not found in database</response>
        /// <response code="500">If an internal server error occurred</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var _itemEntity = await _repositoryWrapper.Item.GetItemById(id);

                if (_itemEntity is null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Item.DeleteItem(_itemEntity);
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

        private async Task<GetItemDto> AddNamesToDtoId(GetItemDto itemMap)
        {
            if (itemMap.OwnerId != null)
            {
                var owner = await _repositoryWrapper.User.GetUserById((int)itemMap.OwnerId);
                itemMap.OwnerName = $"{owner.FirstName} {owner.LastName}";
            }

            if (itemMap.BrandId != null)
            {
                var brand = await _repositoryWrapper.Brand.GetBrandById((int)itemMap.BrandId);
                itemMap.BrandName = brand.Name;
            }

            if (itemMap.DeskId != null)
            {
                var desk = await _repositoryWrapper.Desk.GetDeskById((int)itemMap.DeskId);
                itemMap.DeskName = desk.Name;
            }

            return itemMap;
        }

        private async Task<IEnumerable<GetItemDto>> AddNamesToDtoIdArray(IEnumerable<GetItemDto> itemsMap)
        {
            var owners = await _repositoryWrapper.User.GetAllUsers();
            var brands = await _repositoryWrapper.Brand.GetAllBrands();
            var desks = await _repositoryWrapper.Desk.GetAllDesks();

            foreach (var item in itemsMap)
            {
                if (item.OwnerId != null)
                {
                    var owner = owners.FirstOrDefault(o => o.Id == item.OwnerId);
                    if (owner != null)
                    {
                        item.OwnerName = $"{owner.FirstName} {owner.LastName}";
                    }
                }

                if (item.BrandId != null)
                {
                    item.BrandName = brands.FirstOrDefault(b => b.Id == item.BrandId).Name;
                }

                if (item.DeskId != null)
                {
                    item.DeskName = desks.FirstOrDefault(s => s.Id == item.DeskId).Name;
                }
            }

            return itemsMap;
        }

        #endregion
    }
}