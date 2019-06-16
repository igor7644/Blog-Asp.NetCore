using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class DeleteCategoryCommand : BaseCommand, IDeleteCategoryCommand
    {
        public DeleteCategoryCommand(Context context) : base(context)
        {
        }

        public void Execute(CategoryDTO request)
        {
            var category = Context.Categories.Find(request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            category.IsDeleted = true;
            Context.SaveChanges();
        }
    }
}
