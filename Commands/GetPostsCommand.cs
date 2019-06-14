using Business.Commands;
using Business.DTO;
using Business.Searches;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class GetPostsCommand : BaseCommand, IGetPostsCommand
    {
        public GetPostsCommand(Context context) : base(context)
        {
        }

        public IEnumerable<PostDTO> Execute(PostSearch request)
        {
            var query = Context.Posts.AsQueryable();

            if (request.Title != null)
            {
                query = query.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
            }

            query.Include(p => p.Comments);

            return query.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Comments = p.Comments.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    CommentText = c.CommentText
                })
            });
        }
    }
}
