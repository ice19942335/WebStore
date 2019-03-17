using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Entities.Entities;

namespace WebStore.Interfaces.services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
