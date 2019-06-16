using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Business.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Searches;
using Business.Exceptions;
using Business.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Context _context;
        private IGetCommentsCommand _getCommentsCommand;
        private IGetCommentCommand _getCommentCommand;
        private IAddCommentCommand _addCommentCommand;
        private IEditCommentCommand _editCommentCommand;
        private IDeleteCommentCommand _deleteCommentCommand;

        public CommentsController(Context context, IGetCommentsCommand getCommentsCommand, IGetCommentCommand getCommentCommand, IAddCommentCommand addCommentCommand, IEditCommentCommand editCommentCommand, IDeleteCommentCommand deleteCommentCommand)
        {
            _context = context;
            _getCommentsCommand = getCommentsCommand;
            _getCommentCommand = getCommentCommand;
            _addCommentCommand = addCommentCommand;
            _editCommentCommand = editCommentCommand;
            _deleteCommentCommand = deleteCommentCommand;
        }

        // GET: api/Comments
        [HttpGet]
        public IActionResult Get([FromQuery] CommentSearch search)
        {
            return Ok(_getCommentsCommand.Execute(search));
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var commentDTO = _getCommentCommand.Execute(id);
                return Ok(commentDTO);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult Post([FromBody] CommentDTO dto)
        {
            try
            {
                _addCommentCommand.Execute(dto);

                return Created("/api/comments/" + dto.Id, new CommentDTO
                {
                    Id = dto.Id,
                    CommentText = dto.CommentText
                });
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CommentDTO dto)
        {
            try
            {
                _editCommentCommand.Execute(dto);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(CommentDTO dto)
        {
            try
            {
                _deleteCommentCommand.Execute(dto);
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
