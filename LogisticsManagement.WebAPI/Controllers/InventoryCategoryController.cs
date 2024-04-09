using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryCategoryController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public InventoryCategoryController(IManagerService managerService) 
        {
            _managerService = managerService;
        }


        [HttpGet]
        [Route("Inventories",Name = "GetAllInventories")]
        [ProducesResponseType(200,Type = typeof(List<InventoryCategoryDTO>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<InventoryCategoryDTO> categories =  await _managerService.GetInventoryCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(InventoryCategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Route("{id:int:min(1):max(100)}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(id <= 0 || id > 100)
                {
                    return BadRequest(error: "Id should be between 1 to 100");
                }

                InventoryCategoryDTO category = await _managerService.GetInventoryCategory(id);

                if(category == null)
                {
                    return NotFound("Inventory Category With Id " + id +" Not Found.");
                }

                return Ok(category);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,"An Error occurred while processing the request.");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(InventoryCategoryDTO))]
        public async Task<IActionResult> Post([FromBody] InventoryCategoryDTO category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Invalid category object provided.");
                }

                int result = await _managerService.AddInventoryCategory(category);

                if (result > 0)
                {
                    category.Id = result;
                    return CreatedAtAction("GetById", new { id = result }, category);
                }
                else
                {
                    return StatusCode(500, "Error occurred while adding the inventory category.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
