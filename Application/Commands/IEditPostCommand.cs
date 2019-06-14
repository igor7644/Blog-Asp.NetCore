using Business.DTO;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands
{
    public interface IEditPostCommand : ICommand<PostDTO>
    {
    }
}
