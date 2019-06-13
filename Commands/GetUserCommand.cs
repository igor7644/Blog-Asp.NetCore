using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
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
            var user = Context.Users.Find(request);

            if(user == null)
            {
                throw new EntityNotFoundException();
            }

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };
        }
    }
}
