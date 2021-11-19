using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUsersBL usersBL;

        public UsersController(IUsersBL usersBL)
        {
            this.usersBL = usersBL;

        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterModel userDetails)
        {
            try
            {
                var result = this.usersBL.Register(userDetails);
                if (result)
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added New User Successfully !" });
                }
                else
                {

                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add new user, Try again" });
                }
            }
            catch (Exception ex)
            {

                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });

            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel loginData)
        {
            var result = this.usersBL.Login(loginData);

            string resultMessage = this.usersBL.GenerateToken(loginData.EmailId);
            try
            {
                if (result.Equals("login SuccessFull"))
                {
                    return this.Ok(new { Status = true, Message = "Login SuccessFull !", Data = resultMessage });

                }
                else if (result.Equals("Login Failed"))
                {
                    return this.Ok(new { Status = true, Message = "Login Failed !" });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Incorrect password" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }
        }

        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.usersBL.ForgetPassword(email);

                if (result.UserId > 0)
                {
                    return this.Ok(new ResponseModel<DataResponseModel>() { Status = true, Message = result.message, Data = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result.message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var result = this.usersBL.ResetPassword(resetPasswordModel);
            try
            {
                if (result)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Successfully changed password !" });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Try again !" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }

        }

    }
}