using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public bool AddUserAddress(AddressModel userDetail);
        public bool RemoveFromUserDetails(int addressId);
        List<AddressModel> GetUserDetails(int userId);
        bool EditAddress(AddressModel userDetail);
    }

}
