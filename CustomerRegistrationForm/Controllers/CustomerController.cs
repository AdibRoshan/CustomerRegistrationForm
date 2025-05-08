using CustomerRegistrationForm.Data;
using CustomerRegistrationForm.Interface;
using CustomerRegistrationForm.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerRegistrationForm.Controllers
    {
    [Route ( "api/[controller]" )]
    [ApiController]
    public class CustomerController : ControllerBase
        {
        private readonly IService _service;

        public CustomerController ( IService service )
            {
            _service = service;
            }

        [HttpGet]
        public async Task<IActionResult> GetAll ( )
            {
            var customers = await _service.GetAllAsync ( );
            return Ok ( customers );
            }

        [HttpGet ( "{id}" )]
        public async Task<IActionResult> Get ( int id )
            {
            var customer = await _service.GetByIdAsync ( id );
            if ( customer == null )
                return NotFound ( );
            return Ok ( customer );
            }

        [HttpPost]
        public async Task<IActionResult> Create ( [FromBody] Customer customer )
            {
            if ( !ModelState.IsValid )
                return BadRequest ( ModelState );

            await _service.AddAsync ( customer );
            return CreatedAtAction ( nameof ( Get ) , new { id = customer.Id } , customer );
            }

        [HttpPut ( "{id}" )]
        public async Task<IActionResult> Update ( int id , [FromBody] Customer customer )
            {
            if ( id != customer.Id )
                return BadRequest ( );

            var existing = await _service.GetByIdAsync ( id );
            if ( existing == null )
                return NotFound ( );

            var updatedCustomer = await _service.UpdateAsync ( customer );
            return Ok ( updatedCustomer );
            }

        [HttpDelete ( "{id}" )]
        public async Task<IActionResult> Delete ( int id )
            {
            var customer = await _service.GetByIdAsync ( id );
            if ( customer == null )
                return NotFound ( );

            await _service.DeleteAsync ( id );
            return NoContent ( );
            }
        }
    }
