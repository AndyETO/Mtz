using Dapper;
using MaritzaBusness.Base;
using MaritzaData;
using MaritzaData.ConfigClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaBusness
{
    public class UsersB : BaseBusnessB
    {
        public Response Create(tblUsers model)
        {
            Response response = new Response();

            try
            {
                db.tblUsers.Attach(model);
                db.Entry(model).State=EntityState.Added;
                db.SaveChanges();
                response.Result = Result.Ok;
            }catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }

        public Response Delete(tblUsers model)
        {
            Response response = new Response();
            try
            {
                db.tblUsers.Attach(model);
                db.Entry(model).State=EntityState.Deleted;
                db.SaveChanges();
                response.Result= Result.Ok;
            }catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }

        public Response Update(tblUsers model)
        {

            Response response = new Response();
            try
            {
                db.tblUsers.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                response.Result= Result.Ok;
            }catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }

        //GET'S

        public tblUsers getUserByUserName(string UserName)
        {
            using(IDbConnection dbConnection = new SqlConnection(connection))
            {
                var model = dbConnection.QueryFirstOrDefault<tblUsers>("getUserByUserName", new {UserName }, commandType: CommandType.StoredProcedure);
                return model;
            }
        }

        public bool CheckIfExistsEmail(string Email)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"IF(Exists(SELECT tblUsers.Email FROM tblUsers WHERE Email = @Email))
                                    SELECT 'TRUE'
                                ELSE
                                    SELECT 'FALSE'";

                var model = dbConnection.Query<bool>(query, new { Email }).FirstOrDefault();

                return model;
            }
        }
        public bool CheckIfExistsUserName(string UserName)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"IF(Exists(SELECT tblUsers.UserName FROM tblUsers WHERE UserName = @UserName))
                                    SELECT 'TRUE'
                                ELSE
                                    SELECT 'FALSE'";

                var model = dbConnection.Query<bool>(query, new { UserName }).FirstOrDefault();

                return model;
            }
        }
    }
}
