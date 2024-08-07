using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Services;

namespace Project_GradeBook_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet("{id}/address")]
        public async Task<IActionResult> GetStudentAddress(int id)
        {
            var result = await addressService.GetStudentAddressAsync(id);
            if (result is string)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> UpdateStudentAddress(int id, CreateAddressDto addressDto)
        {
            await addressService.UpdateStudentAddressAsync(id, addressDto);
            return NoContent();
        }
    }
}
