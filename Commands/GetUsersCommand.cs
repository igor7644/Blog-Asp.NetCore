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
    public class GetUsersCommand : BaseCommand, IGetUsersCommand
    {
        public GetUsersCommand(Context context) : base(context)
        {
        }

        public IEnumerable<UserDTO> Execute(UserSearch request)
        {
            var query = Context.Users.Where(u => u.IsDeleted == false).AsQueryable();

            if (request.FirstName != null)
            {
                query = query.Where(u => u.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            }
            if (request.LastName != null)
            {
                query = query.Where(u => u.LastName.ToLower().Contains(request.LastName.ToLower()));
            }
            if (request.Username != null)
            {
                query = query.Where(u => u.Username.ToLower().Contains(request.Username.ToLower()));
            }

            return query.Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username
            }); 
        }
    }
}
