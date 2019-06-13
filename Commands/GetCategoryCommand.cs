using Business.Commands;
using Business.DTO;
using Business.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;


namespace Commands
{
    public class GetCategoryCommand : BaseCommand, IGetCategoryCommand
    {
        public GetCategoryCommand(Context context) : base(context)
        {
        }

        public CategoryDTO Execute(int request)
        {
            var category = Context.Categories.Find(request);

            if(category == null)
            {
                throw new EntityNotFoundException();
            }

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
