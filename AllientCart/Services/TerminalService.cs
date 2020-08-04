using AllientCart.Domain;
using AllientCart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllientCart.Services
{
    public class TerminalService : ITerminal
    {
        public List<Product> Cart { get; set; }
        private readonly IProductService productService;
        public decimal TotalPurchase { get; private set; }

        public TerminalService(IProductService productService)
        {
            this.productService = productService;
            Cart = new List<Product>();
        }


        public void Scan(string item)
        {
            Product p = productService.GetProductByID(item);

            if (p == null)
                throw new Exception($"A product with product code {item} does not exist.");

            Cart.Add(p);
            Total();
        }

        public decimal Total()
        {
            TotalPurchase = productService.GetTotalForCart(Cart);
            return TotalPurchase;
        }
    }
}
