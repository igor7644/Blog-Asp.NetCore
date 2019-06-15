using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class DeleteUserCommand : BaseCommand, IDeleteUserCommand
    {
        public DeleteUserCommand(Context context) : base(context)
        {
        }

        public void Execute(UserDTO request)
        {
            var user = Context.Users.Find(request.Id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            user.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
