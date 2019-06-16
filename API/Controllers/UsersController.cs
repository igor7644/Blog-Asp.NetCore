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
        private IEditUserCommand _editUserCommand;
        private IDeleteUserCommand _deleteUserCommand;

        public UsersController(Context context, IGetUsersCommand getCommand, IGetUserCommand getOneCommand, IAddUserCommand addUserCommand, IEditUserCommand editUserCommand, IDeleteUserCommand deleteUserCommand)
        {
            _context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addUserCommand = addUserCommand;
            _editUserCommand = editUserCommand;
            _deleteUserCommand = deleteUserCommand;
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
            try
            {
                _addUserCommand.Execute(dto);

                return Created("/api/users/" + dto.Id, new UserDTO
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Username = dto.Username
                });
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UserDTO dto)
        {
            try
            {
                _editUserCommand.Execute(dto);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(UserDTO dto)
        {
            try
            {
                _deleteUserCommand.Execute(dto);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
