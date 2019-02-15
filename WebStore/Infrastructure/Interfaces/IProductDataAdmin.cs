using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Entities.Entities;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductDataAdmin
    {
        ProductViewModel Create(ProductViewModel product);

        ProductViewModel Edit(ProductViewModel product);

        void Delete(int id);

        void Details(int id);
    }
}
