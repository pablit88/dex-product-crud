﻿namespace Dex.ProductCrud.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Category> Categories { get; } = [];
    }
}
