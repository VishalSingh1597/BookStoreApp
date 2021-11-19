using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL repository;
        public WishListBL(IWishListRL repository)
        {
            this.repository = repository;
        }

        public bool AddToWishList(WishListModel wishListModel)
        {
            try
            {
                return this.repository.AddToWishList(wishListModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<WishListModel> GetFromWishList(int userId)
        {
            try
            {
                return this.repository.GetFromWishList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveFromWishList(int wishListId)
        {
            try
            {
                return this.repository.RemoveFromWishList(wishListId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}