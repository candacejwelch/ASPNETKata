using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlIntro
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int productId);
        void UpdateProduct(Product prod);
        void InsertProduct(Product prod);
        Product GetDetails(int productId);
    }
}
