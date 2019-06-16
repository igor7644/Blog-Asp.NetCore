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
    public class EditCategoryCommand : BaseCommand, IEditCategoryCommand
    {
        public EditCategoryCommand(Context context) : base(context)
        {
        }

        public void Execute(CategoryDTO request)
        {
            var category = Context.Categories.Find(request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            if (category.Name != request.Name)
            {
                if (Context.Categories.Any(c => c.Name == request.Name))
                {
                    throw new EntityExistException();
                }

                category.Name = request.Name;
            }

            Context.SaveChanges();
        }
    }
}
