﻿namespace eShop.Data.Entities
{
    public class OrderDetail
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
