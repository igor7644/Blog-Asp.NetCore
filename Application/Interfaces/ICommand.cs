using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    //komanda koja nece imati povratnu vrednost
    public interface ICommand<T>
    {
        void Execute(T request);
    }

    //komanda koja ce imati povratnu vrednost
    public interface ICommand<TRequest, TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
