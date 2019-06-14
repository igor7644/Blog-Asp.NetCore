using Business.Commands;
using Business.DTO;
using Business.Searches;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class GetCategoriesCommand : BaseCommand, IGetCategoriesCommand
    {

        public GetCategoriesCommand(Context context) : base(context)
        {
        }

        public IEnumerable<CategoryDTO> Execute(CategorySearch request)
        {
            var query = Context.Categories.AsQueryable();

            if(request.Keyword != null)
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.Keyword.ToLower()));
            }

            query.Include(c => c.Posts);

            return query.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Posts = c.Posts.Select(p => new PostDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description
                })
            });
        }
    }
}
