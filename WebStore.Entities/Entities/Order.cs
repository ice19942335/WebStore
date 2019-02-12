using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using WebStore.DomainNew.Entities;
using WebStore.Entities.Entities.Base;

namespace WebStore.Entities.Entities
{
    public class Order : NamedEntity
    {
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public  DateTime Date { get; set; }

        public  virtual Collection<OrderItem> OrderItems { get; set; }
    }
}
