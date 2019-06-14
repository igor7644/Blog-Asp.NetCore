using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class GetCommentCommand : BaseCommand, IGetCommentCommand
    {
        public GetCommentCommand(Context context) : base(context)
        {
        }

        public CommentDTO Execute(int request)
        {
            var comment = Context.Comments.Find(request);

            if (comment == null)
            {
                throw new EntityNotFoundException();
            }

            return new CommentDTO
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                Post = comment.Post
            };
        }
    }
}
