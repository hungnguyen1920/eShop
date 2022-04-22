using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Data.Entities
{
    public class ProductCategoryMap
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
