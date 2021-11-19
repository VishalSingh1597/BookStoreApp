using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        bool AddToWishList(WishListModel wishListModel);
        bool RemoveFromWishList(int wishListId);
        public List<WishListModel> GetFromWishList(int userId);

    }
}