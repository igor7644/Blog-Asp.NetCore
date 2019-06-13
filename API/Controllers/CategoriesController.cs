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

        public CategoriesController(Context context, IGetCategoriesCommand getCommand, IGetCategoryCommand getOneCommand)
        {
            _context = context;
            _getCommand = getCommand;
            _getOneCommand = getOneCommand;
        }


        // GET api/Categories
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch query)
        {
            return Ok(_getCommand.Execute(query));
        }

        // GET api/categories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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

        // POST api/categories
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _context.Categories.Add(category);

            try
            {
                _context.SaveChanges();

                return Created("/api/categories/" + category.Id, new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            catch
            {
                return StatusCode(500, "An error has occured !!");
            }
        }

        // PUT api/categories/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDTO dto)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.IsDeleted)
            {
                return NotFound();
            }

            if (_context.Categories.Any(c => c.Name == dto.Name))
            {
                return Conflict("Category with that name already exists, try again with different name!");
            }

            category.Name = dto.Name;

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

        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.IsDeleted)
            {
                return Conflict("That category is already deleted!");
            }
                
            category.IsDeleted = true;

            try
            {
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
