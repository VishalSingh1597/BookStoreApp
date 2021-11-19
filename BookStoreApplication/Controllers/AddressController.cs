using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
[ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRL manager;

        public AddressController(IAddressRL manager)
        {
            this.manager = manager;

        }
        [HttpPost]
        [Route("AddUserAddress")]
        public IActionResult AddUserAddress([FromBody] AddressModel userDetail)
        {
            try
            {
                var result = this.manager.AddUserAddress(userDetail);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New UserDetail Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add user Details, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpDelete]
        [Route("RemoveFromUserDetails")]
        public IActionResult RemoveFromUserDetails(int addressId)
        {
            try
            {
                var result = this.manager.RemoveFromUserDetails(addressId);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Removed User Address Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to Remove User Address, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }
        [HttpGet]
        [Route("getUserAddress")]
        public IActionResult getUserAddress(int userId)
        {
            var result = this.manager.GetUserDetails(userId);
            try
            {
                if (result.Count > 0)
                {
                    return this.Ok(new { Status = true, Message = "Address successfully retrived", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "No address available" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }
        [HttpPost]
        [Route("EditAddress")]
        public IActionResult EditAddress([FromBody] AddressModel userDetail)
        {
            var result = this.manager.EditAddress(userDetail);
            try
            {
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Address updated successfully" });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Address updation fails" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

    }
}
