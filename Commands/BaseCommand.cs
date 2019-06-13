using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public abstract class BaseCommand
    {
        protected Context Context { get; }

        protected BaseCommand(Context context)
        {
            this.Context = context;
        }
    }
}
