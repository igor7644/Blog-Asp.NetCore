using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using Business.Searches;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Context _context;
        private IGetCategoriesCommand _getCommand;
        private IGetCategoryCommand _getOneCommand;
        private IAddCategoryCommand _addCommand;
        private IEditCategoryCommand _editCommand;
        private IDeleteCategoryCommand _deleteCommand;

        public CategoriesController(Context context, IGetCategoriesCommand getCommand, IGetCategoryCommand getOneCommand, IAddCategoryCommand addCommand, IEditCategoryCommand editCommand, IDeleteCategoryCommand deleteCommand)
        {
            _context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
            _addCommand = addCommand;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }

        /// <summary>
        /// Returns all Categories
        /// </summary>
        /// 
        ///  <remarks>
        ///  Sample request:
        ///  
        ///     GET /Categories
        ///     {
        ///        "id": 1,
        ///        "Name": "Name 1"
        ///        "Posts": "Posts"
        ///     }
        ///
        /// </remarks>
        // GET api/Categories
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get([FromQuery] CategorySearch query)
        {
            return Ok(_getCommand.Execute(query));
        }

        /// <summary>
        /// Return one category
        /// </summary>
        // GET api/categories/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> Get(int id)
        {
            try
            {
                var categoryDTO = _getOneCommand.Execute(id);
                return Ok(categoryDTO);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Return created category
        /// </summary>
        // POST api/categories
        [HttpPost]
        public ActionResult<IEnumerable<CategoryDTO>> Post([FromBody] CategoryDTO dto)
        {
            try
            {
                _addCommand.Execute(dto);

                return Created("/api/categories/" + dto.Id, new CategoryDTO
                {
                    Id = dto.Id,
                    Name = dto.Name
                });
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        /// <summary>
        /// Return edited category
        /// </summary>
        // PUT api/categories/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> Put(int id, [FromBody] CategoryDTO dto)
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
        /// Return (soft)deleted category
        /// </summary>
        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<CategoryDTO>> Delete(CategoryDTO dto)
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
