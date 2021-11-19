using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL repository;
        public AddressBL(IAddressRL repository)
        {
            this.repository = repository;
        }
        public bool AddUserAddress(AddressModel userDetail)
        {
            try
            {
                return this.repository.AddUserAddress(userDetail);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<AddressModel> GetUserDetails(int userId)
        {
            try
            {
                return this.repository.GetUserDetails(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool EditAddress(AddressModel userDetail)
        {
            try
            {
                return this.repository.EditAddress(userDetail);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool RemoveFromUserDetails(int addressId)
        {
            try
            {
                return this.repository.RemoveFromUserDetails(addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
