using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Web;
using System.Net.Mail;
using System.Net;
using MaritzaData.ConfigClasses;

namespace MaritzaData.Base
{
    public class AuxMethods
    {

        public string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public string GetUserRol(string UserID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                var model = dbConnection.QueryFirstOrDefault<string>("GetUserRol", new { UserID }, commandType: CommandType.StoredProcedure).ToString();
                return model;
            }
        }

        public Response SendEmail(string toAddress, string subject, string htmlBody)
        {
            Response Response = new Response();

            bool validEmail = isEmailValid(toAddress);

            if (validEmail)
            {
                try
                {



                    // Dirección y puerto del servidor SMTP de Gmail
                    string smtpServer = "smtp.gmail.com";
                    int port = 587;

                    string senderEmail = "tu_correo@gmail.com";
                    string password = "tu_contraseña";

                    using (SmtpClient client = new SmtpClient(smtpServer, port))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(senderEmail, password);
                        using (MailMessage message = new MailMessage(senderEmail, toAddress, subject, htmlBody))
                        {
                            message.IsBodyHtml = true;
                            client.Send(message);
                        }
                    }

                    Response.Result = Result.Ok;
                }
                catch (Exception ex)
                {
                    Response.Result = Result.Error;
                    Response.data = ex.Message;
                }
            }
            else
            {
                //Valid address
                Response.Result = Result.Exception;
                Response.data = "Email no valido.";
            }
            return Response;
        }

        public bool isEmailValid(string Email)
        {
            bool validEmail = false;

            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    MailAddress mailAddress = new MailAddress(Email);
                    validEmail = true;
                }catch (Exception ex) 
                {
                    //error
                }
            }


            return validEmail;
        }

    }
}
