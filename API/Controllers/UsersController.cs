using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using Business.Searches;
using DataAccess;
using Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        private IGetUsersCommand _getCommand;
        private IGetUserCommand _getOneCommand;
        private IAddUserCommand _addUserCommand;

        public UsersController(Context context, IGetUsersCommand getCommand, IGetUserCommand getOneCommand, IAddUserCommand addUserCommand)
        {
            _context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addUserCommand = addUserCommand;
        }



        // GET: api/Users
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch query)
        {
            return Ok(_getCommand.Execute(query));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var userDTO = _getOneCommand.Execute(id);
                return Ok(userDTO);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO dto)
        {
            var user = new UserDTO
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username
            };

            _addUserCommand.Execute(user);

            try
            {
                _context.SaveChanges();

                return Created("/api/users/" + user.Id, new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
                });
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO dto)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.IsDeleted)
            {
                return NotFound();
            }

            if (_context.Users.Any(u => u.Username == dto.Username))
            {
                return Conflict("User with that username already exists!");
            }

            user.Username = dto.Username;

            try
            {
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
