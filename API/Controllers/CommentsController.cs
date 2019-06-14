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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly Context _context;
        private IGetCommentsCommand _getCommentsCommand;
        private IGetCommentCommand _getCommentCommand;

        public CommentsController(Context context, IGetCommentsCommand getCommentsCommand, IGetCommentCommand getCommentCommand)
        {
            _context = context;
            _getCommentsCommand = getCommentsCommand;
            _getCommentCommand = getCommentCommand;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Comments/5
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
