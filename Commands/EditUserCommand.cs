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
    public class EditUserCommand : BaseCommand, IEditUserCommand
    {
        public EditUserCommand(Context context) : base(context)
        {
        }

        public void Execute(UserDTO request)
        {
            var user = Context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            if (user.Username != request.Username)
            {
                if (Context.Users.Any(u => u.Username == request.Username))
                {
                    throw new EntityExistException();
                }

                user.Username = request.Username;
            }

            if (user.FirstName != request.FirstName)
            {
                user.FirstName = request.FirstName;
            }

            if (user.LastName != request.LastName)
            {
                user.LastName = request.LastName;
            }

            Context.SaveChanges();
        }
    }
}
