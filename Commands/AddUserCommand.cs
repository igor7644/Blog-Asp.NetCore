using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class AddUserCommand : BaseCommand, IAddUserCommand
    {
        public AddUserCommand(Context context) : base(context)
        {
        }

        public void Execute(UserDTO request)
        {
            if (Context.Users.Any(u => u.Username == request.Username))
            {
                throw new EntityExistException();
            }

            Context.Users.Add(new Domain.User
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            });

            Context.SaveChanges();
        }
    }
}
