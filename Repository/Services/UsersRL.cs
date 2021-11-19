using CommonLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UsersRL : IUsersRL
    {
        public UsersRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        public bool Register(RegisterModel userDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    var password = EncryptPassword(userDetails.Password);
                    string encryptedPassword = StringCipher.Encrypt(userDetails.Password);
                    SqlCommand sqlCommand = new SqlCommand("dbo.UserRegistration", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@FullName", userDetails.FullName);
                    sqlCommand.Parameters.AddWithValue("@EmailId", userDetails.EmailId);
                    sqlCommand.Parameters.AddWithValue("@Password", password);
                    sqlCommand.Parameters.AddWithValue("@PhoneNo", userDetails.PhoneNo);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Password Encryption" + ex.Message);
            }
        }
        public string Login(LoginModel loginData)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            try
            {
                var encodedpassword = EncryptPassword(loginData.Password);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("spUserLogin", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@email", loginData.EmailId);
                cmd.Parameters.AddWithValue("@password", encodedpassword);
                var returnedSQLParameter = cmd.Parameters.Add("@user", SqlDbType.Int);
                returnedSQLParameter.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var result = cmd.Parameters["@user"].Value;
                if (!(result is DBNull))

                {
                    
                    //RegisterModel customer = new RegisterModel();
                    //SqlDataReader rd = cmd.ExecuteReader();
                    if (Convert.ToInt32(result)==2)
                    {
                       
                        //customer.UsersId = rd.GetInt32("userId");
                        //customer.FullName = rd.GetString("FullName");
                        //customer.PhoneNo = rd.GetInt64("PhoneNo").ToString();
                        //customer.EmailId = rd.GetString("EmailId");
                        //customer.Password = rd.GetString("Password");
                        //customer.ISAdmin = false;
                        return "login SuccessFull";
                    }
                    return "Incorrect Password";
                }
                //else if (result.Equals(3))
                //{
                //    RegisterModel Admin = new RegisterModel();
                //    SqlDataReader rd = cmd.ExecuteReader();
                //    if (rd.Read())
                //    {
                //        Admin.UsersId = rd.GetInt32(0);
                //        Admin.FullName = rd.GetString(1);
                //        Admin.PhoneNo = rd.GetInt64(4).ToString();
                //        Admin.EmailId = rd.GetString(2);
                //        Admin.Password = rd.GetString(3);
                //        Admin.ISAdmin = true;
                //        return Admin;
                //    }
                //    return null;
                //}
                else
                {
                    return "Login Failed";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        //public RegisterModel Login(LoginModel loginData)
        //{
        //    sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
        //    try
        //    {
        //        string encryptedPassword = StringCipher.Encrypt(password);
        //        SqlCommand cmd = new SqlCommand("UserLogin", connection);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Email", email);
        //        cmd.Parameters.AddWithValue("@Password", encryptedPassword);

        //        SqlParameter userId = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
        //        userId.Direction = System.Data.ParameterDirection.Output;
        //        SqlParameter emailout = new SqlParameter("@EmailOut", System.Data.SqlDbType.VarChar, 30);
        //        emailout.Direction = System.Data.ParameterDirection.Output;

        //        cmd.Parameters.Add(userId);
        //        cmd.Parameters.Add(emailout);

        //        connection.Open();
        //        cmd.ExecuteNonQuery();
        //        string ID = (cmd.Parameters["@UserId"].Value).ToString();
        //        string Emailout = (cmd.Parameters["@EmailOut"].Value).ToString();

        //        connection.Close();
        //        connection.Dispose();


        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var tokenKey = Encoding.ASCII.GetBytes("Hello This Token Is Genereted");
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //       {
        //            new Claim("Email",Emailout),
        //            new Claim("UserId",ID)
        //       }),
        //            Expires = DateTime.UtcNow.AddHours(7),
        //            SigningCredentials =
        //       new SigningCredentials(
        //           new SymmetricSecurityKey(tokenKey),
        //           SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var token = tokenHandler.CreateToken(tokenDescriptor);

        //        return tokenHandler.WriteToken(token);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public bool ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdatePassword", sqlConnection);
                    //passing command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    //adding the parameter to the strored procedure
                    var password = EncryptPassword(resetPasswordModel.NewPassword);
                    sqlCommand.Parameters.AddWithValue("@UserId", resetPasswordModel.UserId);
                    sqlCommand.Parameters.AddWithValue("@NewPassword", password);
                    //checking the result 
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        public DataResponseModel ForgetPassword(string email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("UserDbConnection"));

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("dbo.EmailValidity", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmailId", email);
                cmd.Parameters.Add("@result", SqlDbType.Int);
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var result = cmd.Parameters["@result"].Value;

                if (result != null && result.Equals(1))
                {
                    var userId = Convert.ToInt32(cmd.Parameters["@userId"].Value);
                    Random random = new Random();
                    int OTP = random.Next(1000, 9999);
                    this.MSMQSend(OTP);
                    if (this.SendEmail(email))
                    {
                        return new DataResponseModel() { UserId = userId, message = "Otp is send to Email", otp = OTP };
                    }
                    else
                    {
                        return new DataResponseModel() { message = "Sent email failed" };
                    }
                }
                else
                {
                    return new DataResponseModel() { message = "Invalid email id" };

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private MessageQueue QueueDetail()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\ResetPasswordQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\ResetPasswordQueue");
            }

            return messageQueue;
        }
        private bool SendEmail(string email)
        {
            string linkToBeSend = this.ReceiveQueue(email);
            if (this.SendMailUsingSMTP(email, linkToBeSend))
            {
                return true;
            }

            return false;
        }

        private void MSMQSend(int otp)
        {
            try
            {
                MessageQueue messageQueue = this.QueueDetail();
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = otp;
                messageQueue.Label = "Otp for password reset";
                messageQueue.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string ReceiveQueue(string email)
        {
            var receiveQueue = new MessageQueue(@".\Private$\ResetPasswordQueue");
            var receiveMsg = receiveQueue.Receive();
            receiveMsg.Formatter = new BinaryMessageFormatter();

            string linkToBeSend = receiveMsg.Body.ToString();
            return linkToBeSend;
        }

        private bool SendMailUsingSMTP(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress("vishalr23singh@gmail.com");
            mailMessage.To.Add(new System.Net.Mail.MailAddress(email));
            mailMessage.Subject = "Link to reset you password for BookStore Application";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "Here is the otp : " + message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("vishalr23singh@gmail.com", "9930315160");
            smtp.Send(mailMessage);
            return true;
        }

        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey:Key"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
