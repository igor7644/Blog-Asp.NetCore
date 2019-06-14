using Business.Commands;
using Business.DTO;
using Business.Searches;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands
{
    public class GetCommentsCommand : BaseCommand, IGetCommentsCommand
    {
        public GetCommentsCommand(Context context) : base(context)
        {
        }

        public IEnumerable<CommentDTO> Execute(CommentSearch request)
        {
            var query = Context.Comments.AsQueryable();

            if(request.Comment != null)
            {
                query = query.Where(c => c.CommentText.ToLower().Contains(request.Comment.ToLower()));
            }

            return query.Select(c => new CommentDTO
            {
                Id = c.Id,
                CommentText = c.CommentText
            });
        }
    }
}
