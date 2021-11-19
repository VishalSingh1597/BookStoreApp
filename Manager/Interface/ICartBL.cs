using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        bool DeleteFromCart(int cartId);
        bool AddToCart(CartModel details);
        List<CartModel> GetCartItems(int userId);
        bool UpdateOrderCount(CartModel cartDetail);
    }
}