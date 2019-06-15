using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class DeletePostCommand : BaseCommand, IDeletePostCommand
    {
        public DeletePostCommand(Context context) : base(context)
        {
        }

        public void Execute(PostDTO request)
        {
            var post = Context.Posts.Find(request.Id);

            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            post.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
