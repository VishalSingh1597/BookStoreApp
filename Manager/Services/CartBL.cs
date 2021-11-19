using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL repository;
        public CartBL(ICartRL repository)
        {
            this.repository = repository;
        }
        public bool AddToCart(CartModel details)
        {
            try
            {
                return this.repository.AddToCart(details);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteFromCart(int cartId)
        {
            try
            {
                return this.repository.DeleteFromCart(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CartModel> GetCartItems(int userId)
        {
            try
            {
                return this.repository.GetCartItems(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateOrderCount(CartModel cartDetail)
        {
            try
            {
                return this.repository.UpdateOrderCount(cartDetail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
