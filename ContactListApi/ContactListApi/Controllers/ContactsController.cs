using ContactListApi.ServiceInterfaces;
using ContactListApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository repo;

        public ContactsController(IContactsRepository repository)
        {
            repo = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetAll() => Ok(repo.GetAll());
        

        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var person = repo.GetById(id);

            if (person is null)
                return NotFound();

            return Ok(person);
        }

        [HttpGet("findByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByNameFilter([FromQuery(Name ="nameFilter")] string nameFilter)
        {
            if (nameFilter is null)
                return BadRequest();

            return Ok(repo.GetByNameFilter(nameFilter));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPerson([FromBody] Person person)
        {
            if (person.Email is null)
                return BadRequest();

            repo.Add(person);
            return Created(nameof(GetById), person);
        }

        [HttpDelete("{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int personId)
        {
            try
            {
                var person = GetById(personId);
            } catch(ArgumentException)
            {
                return BadRequest();
            }
            
            repo.Delete(personId);
            return NoContent();
        }
    }
}
