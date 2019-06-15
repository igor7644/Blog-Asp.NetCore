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
    public class EditPostCommand : BaseCommand, IEditPostCommand
    {
        public EditPostCommand(Context context) : base(context)
        {
        }

        public void Execute(PostDTO request)
        {
            var post = Context.Posts.Find(request.Id);

            if(post == null)
            {
                throw new EntityNotFoundException();
            }

            if(post.Title != request.Title)
            {
                if (Context.Posts.Any(p => p.Title == request.Title))
                {
                    throw new EntityExistException();
                }
                
                post.Title = request.Title;    
            }

            if(post.Description != request.Description)
            {
                post.Description = request.Description;
            }

            Context.SaveChanges();
        }
    }
}
