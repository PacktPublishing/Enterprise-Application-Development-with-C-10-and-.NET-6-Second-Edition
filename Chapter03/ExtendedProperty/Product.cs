using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedProperty
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Address Location { get; set; }
    }

    public class Address
    {
        public string State { get; set; }
        public string Country { get; set; }
    }
}
