using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ICartRL
    {
        bool AddToCart(CartModel details);
        bool DeleteFromCart(int cartId);
        bool UpdateOrderCount(CartModel cartDetail);
        List<CartModel> GetCartItems(int userId);
    }
}
