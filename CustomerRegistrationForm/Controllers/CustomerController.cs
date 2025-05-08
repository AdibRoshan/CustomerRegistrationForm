using CustomerRegistrationForm.Data;
using CustomerRegistrationForm.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistrationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
        {
        private readonly CustomerRepository _repository;

        public CustomerController ( CustomerRepository repository )
            {
            _repository = repository;
            }

        
        [HttpGet]
        public IActionResult GetAll ( )
            {
            var customers = _repository.GetAll ( );
            return Ok ( customers );
            }

      
        [HttpGet ( "{id}" )]
        public IActionResult Get ( int id )
            {
            var customer = _repository.GetById ( id );
            if ( customer == null )
                return NotFound ( );
            return Ok ( customer );
            }

       
        [HttpPost]
        public IActionResult Create ( [FromBody] Customer customer )
            {
            if ( !ModelState.IsValid )
                return BadRequest ( ModelState );

            _repository.Add ( customer );
            return CreatedAtAction ( nameof ( Get ) , new { id = customer.Id } , customer );
            }

       
        [HttpPut ( "{id}" )]
        public IActionResult Update ( int id , [FromBody] Customer customer )
            {
            if ( id != customer.Id )
                return BadRequest ( );

            var existing = _repository.GetById ( id );
            if ( existing == null )
                return NotFound ( );

            var updatedCustomer = _repository.Update ( customer );

            return Ok ( updatedCustomer );
            }


       
        [HttpDelete ( "{id}" )]
        public IActionResult Delete ( int id )
            {
            var customer = _repository.GetById ( id );
            if ( customer == null )
                return NotFound ( );

            _repository.Delete ( id );
            return NoContent ( );
            }
        }
    }
