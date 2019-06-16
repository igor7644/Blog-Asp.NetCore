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
    public class AddCategoryCommand : BaseCommand, IAddCategoryCommand
    {
        public AddCategoryCommand(Context context) : base(context)
        {
        }

        public void Execute(CategoryDTO request)
        {
            if (Context.Categories.Any(c => c.Name == request.Name))
            {
                throw new EntityExistException();
            }

            Context.Categories.Add(new Domain.Category
            {
                Name = request.Name
            });

            Context.SaveChanges();
        }
    }
}
