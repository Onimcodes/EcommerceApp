using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.Interfaces.Persistence
{
    public  interface IUnitOfWork : IDisposable 
    {
        IProductRepository Products { get; }

    }
}
