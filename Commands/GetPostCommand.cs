using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
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
            var post = Context.Posts.Find(request);

            if(post == null)
            {
                throw new EntityNotFoundException();
            }

            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description
            };
        }
    }
}
