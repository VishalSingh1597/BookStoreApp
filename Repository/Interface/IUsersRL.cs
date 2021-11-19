using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUsersRL
        {

            bool Register(RegisterModel userDetails);
           string Login(LoginModel loginData);
            DataResponseModel ForgetPassword(string email);
            bool ResetPassword(ResetPasswordModel resetPasswordModel);
            string GenerateToken(string email);
        }
    }
