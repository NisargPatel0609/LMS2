using Azure;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly IManagerService _managerService;
        public InventoryController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] InventoryDTO inventory)
        {
            try
            {
                if (inventory == null)
                {
                    return BadRequest();
                }
                if (inventory.Stock <= 0)
                {
                    return BadRequest();
                }
                if (await _managerService.PutInventory(inventory) > 0)
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }




        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<InventoryDTO> patchInventory)
        {
            try
            {
                if (patchInventory == null)
                {
                    return BadRequest();
                }
                if (id <= 0)
                {
                    return BadRequest();
                }
                InventoryDTO inv = await _managerService.GetInventory(id);
                Console.WriteLine("Name : " + inv.Name);
                if (inv == null)
                {
                    return NotFound();
                }
                patchInventory.ApplyTo(inv);
                if (await _managerService.UpdateInventory(inv) > 0)
                {
                    return NoContent();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }
                if(await _managerService.RemoveInventory(id) > 0)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
