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
    public class GetUserCommand : BaseCommand, IGetUserCommand
    {
        public GetUserCommand(Context context) : base(context)
        {
        }

        public UserDTO Execute(int request)
        {
            var user = Context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == request);

            if(user == null)
            {
                throw new EntityNotFoundException();
            }

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Posts = user.Posts.Select(p => new PostDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description
                })
            };
        }
    }
}
