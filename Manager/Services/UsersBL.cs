using BusinessLayer.Interface;
using CommonLayer;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services
{
    public class UsersBL : IUsersBL
        {
            private readonly IUsersRL repository;
            public UsersBL(IUsersRL repository)
            {
                this.repository = repository;
            }
            public bool Register(RegisterModel userDetails)
            {
                try
                {
                    return this.repository.Register(userDetails);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        public string Login(LoginModel logindata)
        {
            try
            {
                return this.repository.Login(logindata);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ResetPassword(ResetPasswordModel resetPasswordModel)
            {
                try
                {
                    return this.repository.ResetPassword(resetPasswordModel);

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public DataResponseModel ForgetPassword(string email)
            {

                try
                {
                    return this.repository.ForgetPassword(email);

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }


            }

        public string GenerateToken(string email)
        {
            try
            {
                return this.repository.GenerateToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
    }
