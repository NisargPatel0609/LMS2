using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public UserController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        [HttpGet]
        [Route("Users", Name = "GetAllUsers")]
        [ProducesResponseType(200, Type = typeof(List<UserWithDetailDTO>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<UserWithDetailDTO> users = await _managerService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
