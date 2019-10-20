using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Category
    {
        #region Properties
        public int CategoryId { get; set; }
        public string Name { get; set; }
        //category contains 1..many products (a product can belong to 1..many categories)
        public ICollection<CategoryProduct> Products { get; private set; }

        #endregion

        #region Constructor and Methods
        public Category(string name) 
        {
            Products = new List<CategoryProduct>();
            Name = name;
        }


        public void AddProduct(string name, decimal price, string description, string thumbnail = null)
        {
            if (Products.FirstOrDefault(p => p.Product.Name == name) == null)
                if (thumbnail == null)
                    Products.Add(new CategoryProduct(this, new Product(name, price, description)));
                else
                    Products.Add(new CategoryProduct(this, new OnlineProduct(name, price, thumbnail, description)));
        }

        public void AddProduct(Product product)
        {
            if (FindProduct(product.Name) == null)
                Products.Add(new CategoryProduct(this, product));
        }

        public void RemoveProduct(Product product)
        {
            CategoryProduct cp = Products.FirstOrDefault(p => p.Product == product);
            Products.Remove(cp);
        }

        public Product FindProduct(string name)
        {
            CategoryProduct cp = Products.FirstOrDefault(p => p.Product.Name == name);
            return cp?.Product;
        }
        #endregion
    }
}
