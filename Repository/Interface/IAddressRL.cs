using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        bool AddUserAddress(AddressModel userDetail);
        bool RemoveFromUserDetails(int addressId);
        List<AddressModel> GetUserDetails(int userId);
        bool EditAddress(AddressModel userDetail);
    }
}
