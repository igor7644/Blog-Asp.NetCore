using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using Business.Searches;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Context _context;
        private IGetPostsCommand _getCommand;
        private IGetPostCommand _getOneCommand;
        private IAddPostCommand _addCommand;

        public PostsController(Context context, IGetPostsCommand getCommand, IGetPostCommand getOneCommand, IAddPostCommand addCommand)
        {
            _context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
        }


        // GET: api/Posts
        [HttpGet]
        public IActionResult Get([FromQuery] PostSearch query)
        {
            return Ok(_getCommand.Execute(query));
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var postDTO = _getOneCommand.Execute(id);
                return Ok(postDTO);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Posts
        [HttpPost]
        public IActionResult Post([FromBody] PostDTO dto)
        {
            try
            {
                _addCommand.Execute(dto);

                return Created("/api/posts/" + dto.Id, new PostDTO
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Description = dto.Description
                });
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
