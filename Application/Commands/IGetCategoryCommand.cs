using Business.DTO;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Business.Commands
{
    public interface IGetCategoryCommand : ICommand<int, CategoryDTO>
    {

    }
}
