using System;
using System.Collections.Generic;

namespace eShop.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<ProductCategoryMap> ProductCategoryMaps { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductTranslation> ProductTranslations { get; set; }
    }
}
