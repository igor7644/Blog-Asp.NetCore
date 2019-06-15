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
    public class GetPostCommand : BaseCommand, IGetPostCommand
    {
        public GetPostCommand(Context context) : base(context)
        {
        }

        public PostDTO Execute(int request)
        {
            var post = Context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == request);

            if(post == null)
            {
                throw new EntityNotFoundException();
            }

            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Comments = post.Comments.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    CommentText = c.CommentText
                })
            };
        }
    }
}
