using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SqlIntro
{
    public class DapperDb : IProductRepository
    {
        private readonly IDbConnection conn;

        public DapperDb(IDbConnection conn)
        {
            this.conn = conn;
        }

        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            return conn.Query<Product>("select * from product ORDER BY ProductId DESC");
        }

        public Product GetDetails(int productId)
        {
            return conn.Query<Product>("select * from product where ProductID = @id", new { id = productId }).FirstOrDefault();
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(int productId)
        {
                conn.Execute("delete from product where ProductID = @id", new {id = productId});
        }

        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
                conn.Execute("update product set name = @name where ProductID = @id",
                    new {id = prod.ProductId, name = prod.Name});
        }

        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
                conn.Execute("INSERT into product (Name) values(@Name)", prod);
        }

        public IEnumerable<Product> LeftJoin()
        { 
            return conn.Query<Product>("select * from product p left join productreview pr on p.Productid = pr.ProductId");
        }

        public IEnumerable<Product> InnerJoin()
        {
            return conn.Query<Product>("select pr.*, p.Name from product p inner join productreview pr on p.Productid = pr.ProductId");
        }

    }
}
