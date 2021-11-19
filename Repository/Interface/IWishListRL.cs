using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        bool AddToWishList(WishListModel wishListModel);
        bool RemoveFromWishList(int wishListId);
        public List<WishListModel> GetFromWishList(int userId);
    }
}