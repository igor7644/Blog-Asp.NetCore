using Business.DTO;
using Business.Interfaces;
using Business.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands
{
    public interface IGetUsersCommand : ICommand<UserSearch, IEnumerable<UserDTO>>
    {

    }
}
