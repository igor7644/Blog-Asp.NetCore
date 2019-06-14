using Business.DTO;
using Business.Interfaces;
using Business.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands
{
    public interface IGetCommentsCommand : ICommand<CommentSearch, IEnumerable<CommentDTO>>
    {

    }
}
