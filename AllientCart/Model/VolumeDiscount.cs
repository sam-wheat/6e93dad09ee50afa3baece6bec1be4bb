using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllientCart.Model
{
    public class VolumeDiscount
    {
        public int MinQty { get; set; }         // Pricing is good for multiples of this quantity
        public decimal Price { get; set; }
    }
}
