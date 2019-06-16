using Business.Commands;
using Business.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class AddCommentCommand : BaseCommand, IAddCommentCommand
    {
        public AddCommentCommand(Context context) : base(context)
        {
        }

        public void Execute(CommentDTO request)
        {
            Context.Comments.Add(new Domain.Comment
            {
                CommentText = request.CommentText
            });

            Context.SaveChanges();
        }
    }
}
