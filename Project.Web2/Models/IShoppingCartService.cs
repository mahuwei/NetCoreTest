using System;
using System.Collections.Generic;

namespace Project.Web2.Models {
    public interface IShoppingCartService {
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem GetById(Guid id);
        void Remove(Guid id);
    }
}