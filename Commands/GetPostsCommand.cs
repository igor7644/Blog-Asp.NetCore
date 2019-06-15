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
            var query = Context.Posts.Where(p => p.IsDeleted == false).AsQueryable();

            if (request.Title != null)
            {
                query = query.Where(p => p.Title.ToLower().Contains(request.Title.ToLower()));
            }

            return query.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            });
        }
    }
}
