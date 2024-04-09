using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public VehicleController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        [HttpGet]
        [Route("Vehicles", Name = "GetAllVehicles")]
        [ProducesResponseType(200, Type = typeof(List<VehicleDTO>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<VehicleDTO> vehicles = await _managerService.GetVehicles();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

    }
}
