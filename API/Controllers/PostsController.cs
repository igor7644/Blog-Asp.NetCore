using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using Business.Interfaces;
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
        private IEditPostCommand _editCommand;
        private IDeletePostCommand _deleteCommand;
        private readonly IEmailSender _emailSender;

        public PostsController(Context context, IGetPostsCommand getCommand, IGetPostCommand getOneCommand, IAddPostCommand addCommand, IEditPostCommand editCommand, IDeletePostCommand deleteCommand, IEmailSender emailSender)
        {
            _context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Returns all Posts
        /// </summary>
        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> Get([FromQuery] PostSearch query)
        {
            return Ok(_getCommand.Execute(query));
        }

        /// <summary>
        /// Return one post
        /// </summary>
        // GET: api/Posts/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PostDTO>> Get(int id)
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

        /// <summary>
        /// Return created post
        /// </summary>
        // POST: api/Posts
        [HttpPost]
        public ActionResult<IEnumerable<PostDTO>> Post([FromBody] PostDTO dto)
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

                _emailSender.Subject = "Success!";
                _emailSender.Body = "Post was successfully created !";
                _emailSender.ToEmail = "netcoreict@gmail.com";
                _emailSender.Send();
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        /// <summary>
        /// Return edited post
        /// </summary>
        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<PostDTO>> Put([FromBody] PostDTO dto)
        {
            try
            {
                _editCommand.Execute(dto);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        /// <summary>
        /// Return (soft)deleted post
        /// </summary>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<PostDTO>> Delete(PostDTO dto)
        {
            try
            {
                _deleteCommand.Execute(dto);
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
