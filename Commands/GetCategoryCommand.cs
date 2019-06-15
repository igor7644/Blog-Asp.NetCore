using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Commands
{
    public class GetCategoryCommand : BaseCommand, IGetCategoryCommand
    {
        public GetCategoryCommand(Context context) : base(context)
        {
        }

        public CategoryDTO Execute(int request)
        {
            var category = Context.Categories.Include(c => c.Posts).Include("Posts.User").FirstOrDefault(c => c.Id == request);

            if(category == null)
            {
                throw new EntityNotFoundException();
            }

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Posts = category.Posts.Select(p => new PostDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    User = new UserDTO
                    {
                        Id = p.User.Id,
                        FirstName = p.User.FirstName,
                        LastName = p.User.LastName,
                        Username = p.User.Username
                    }
                })
            };
        }
    }
}
