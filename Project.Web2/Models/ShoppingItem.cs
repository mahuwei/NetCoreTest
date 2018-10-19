using System;

namespace Project.Web2.Models {
    public class ShoppingItem {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
    }
}