using Business.DTO;
using Business.Interfaces;
using Business.Searches;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Business.Commands
{
    public interface IGetCategoriesCommand : ICommand<CategorySearch, IEnumerable<CategoryDTO>>
    {

    }
}
