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
    public class AddPostCommand : BaseCommand, IAddPostCommand
    {
        public AddPostCommand(Context context) : base(context)
        {
        }

        public void Execute(PostDTO request)
        {
            if(Context.Posts.Any(p => p.Title == request.Title))
            {
                throw new EntityExistException();
            }

            Context.Posts.Add(new Domain.Post
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                CategoryId = request.CategoryId
            });

            Context.SaveChanges();
        }
    }
}
