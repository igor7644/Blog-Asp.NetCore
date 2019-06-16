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

        /// <summary>
        /// Returns all Users
        /// </summary>
        /// 
        ///  <remarks>
        ///  Sample request:
        ///  
        ///     GET /Users
        ///     {
        ///        "id": 1,
        ///        "FirstName": "Name",
        ///        "LastName": "Last Name",
        ///        "Username": "Username",
        ///        "Posts": "Posts"
        ///     }
        ///
        /// </remarks>
        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get([FromQuery] UserSearch query)
        {
            return Ok(_getCommand.Execute(query));
        }

        /// <summary>
        /// Return one user
        /// </summary>
        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<IEnumerable<UserDTO>> Get(int id)
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

        /// <summary>
        /// Return created user
        /// </summary>
        // POST: api/Users
        [HttpPost]
        public ActionResult<IEnumerable<UserDTO>> Post([FromBody] UserDTO dto)
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

        /// <summary>
        /// Return edited user
        /// </summary>
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<UserDTO>> Put([FromBody] UserDTO dto)
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

        /// <summary>
        /// Return (soft)deleted user
        /// </summary>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<UserDTO>> Delete(UserDTO dto)
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
