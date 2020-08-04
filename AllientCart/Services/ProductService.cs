using AllientCart.Domain;
using AllientCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllientCart.Services
{
    public class ProductService : IProductService
    {
        private Dictionary<string, VolumeDiscount[]> Prices;
        private List<Product> Products;

        public ProductService() => Initialize();

        private void Initialize()
        {

            // Load prices from a database...
            Products = new List<Product> {
                new Product { ProductCode = "A" },
                new Product { ProductCode = "B" },
                new Product { ProductCode = "C" },
                new Product { ProductCode = "D" }
            };

            Prices = new Dictionary<string, VolumeDiscount[]>();
            
            
            // For  lack of instruction to the contrary I interpret the instructions for Product A to be the
            // same as for product C:
            // "$2 each or $1.75 each for multiples of four (only)."

            // Volume discounts are added in ascending order.
            // The first price is always for a quantity of one since that is the default quantity.  Using this allows us to bypassing scanning 
            // the VolumeDiscount array when the customer purchases a quantity of one.

            Prices["A"] = new VolumeDiscount[2] { new VolumeDiscount { MinQty = 1, Price = 2m }, new VolumeDiscount { MinQty = 4, Price = 1.75m } };
            Prices["B"] = new VolumeDiscount[1] { new VolumeDiscount { MinQty = 1, Price = 12.00m } };
            Prices["C"] = new VolumeDiscount[2] { new VolumeDiscount { MinQty = 1, Price = 1.25m }, new VolumeDiscount { MinQty = 6, Price = 1.0m } };
            Prices["D"] = new VolumeDiscount[1] { new VolumeDiscount { MinQty = 1, Price = .15m } };
        }

        public List<Product> GetProducts() => Products;

        public Product GetProductByID(string productCode) => Products.FirstOrDefault(x => x.ProductCode == productCode);

        public decimal GetTotalForCart(List<Product> cart)
        {
            if (cart == null || !cart.Any())
                return 0m;

            decimal total = cart.GroupBy(x => x.ProductCode).Sum(y => GetVolumePrice(y.Key, y.Count()));
            return total;
        }

        public decimal GetVolumePrice(string productCode, int qtyPurchased)
        {
            if (String.IsNullOrEmpty(productCode))
                throw new ArgumentNullException("productCode is required.");

            if(qtyPurchased == 0)
                throw new ArgumentException("quantity cannot be zero.");

            // Customer may be returning some items in which case qtyPurchased will be less than zero.  
            // We need to be able to credit them the correct amount.  Use absolute value of qty for lookups:

            int qty = Math.Abs(qtyPurchased);
            VolumeDiscount[] priceBreaks = null;
            decimal totalPrice, price = 0;
            

            // Get the Volume Discount for the product being purchased:

            if (!Prices.TryGetValue(productCode, out  priceBreaks))
                throw new Exception($"ProductCode {productCode} was not found in the pricing table.");

            // If customer is purchasing a quantity of one we know the price is the first price break.
            // For quantities greater than one we take the greatest price break where the minimum quantity that must be purchased is
            // less than or equal to the quantity the customer actually purchased.  
            // Recall that VolumeDiscounts are in ascending order.

            if (qty == 1)
            {
                price = priceBreaks[0].Price;
                totalPrice =  qtyPurchased * price;
            }
            else
            {
                VolumeDiscount volDis = priceBreaks.Last(y => y.MinQty <= qty);
                int multiple = qty / volDis.MinQty;
                int remainder = qty - (multiple * volDis.MinQty);

                // Call this method recursively with remainders, finding each successively lower price break.
                totalPrice = (multiple * volDis.MinQty * volDis.Price) + (remainder == 0 ? 0 : GetVolumePrice(productCode, remainder));
            }
            return totalPrice;
        }
    }
}
