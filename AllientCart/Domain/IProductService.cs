
using AllientCart.Model;
using System.Collections.Generic;

namespace AllientCart.Domain
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductByID(string productCode);
        decimal GetTotalForCart(List<Product> cart);
        decimal GetVolumePrice(string productCode, int qtyPurchased);
    }
}
