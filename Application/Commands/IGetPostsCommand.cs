using Business.DTO;
using Business.Interfaces;
using Business.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands
{
    public interface IGetPostsCommand : ICommand<PostSearch, IEnumerable<PostDTO>>
    {

    }
}
