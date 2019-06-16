using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class DeleteCommentCommand : BaseCommand, IDeleteCommentCommand
    {
        public DeleteCommentCommand(Context context) : base(context)
        {
        }

        public void Execute(CommentDTO request)
        {
            var comment = Context.Comments.Find(request.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException();
            }

            comment.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
