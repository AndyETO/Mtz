using MaritzaBusness.Base;
using MaritzaData.ConfigClasses;
using MaritzaData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace MaritzaBusness
{
    public class ProyectLikesB : BaseBusnessB
    {
        public Response Create(tblProyectLikes model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectLikes.Attach(model);
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                response.Result = Result.Ok;
            }
            catch (Exception ex)
            {
                response.Result = Result.Exception;
                response.data = ex.Message;
            }
            return response;
        }
        public Response Update(tblProyectLikes model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectLikes.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                response.Result = Result.Ok;
            }
            catch (Exception ex)
            {
                response.Result = Result.Exception;
                response.data = ex.Message;
            }
            return response;
        }

        public bool CheckIfExistsByUserIDAndProyectDetailsID(int UserID, int ProyectDetailID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"IF(Exists(
                                SELECT 
                                      UserID
                                      ,ProyectDetailID
                                  FROM tblProyectLikes
                                  WHERE
                                  tblProyectLikes.Active = 1 AND
                                  tblProyectLikes.UserID = @UserID AND
                                  tblProyectLikes.ProyectDetailID = @ProyectDetailID 
                                ))
                                    SELECT 'TRUE'
                                ELSE
                                    SELECT 'FALSE'";

                var model = dbConnection.Query<bool>(query, new { UserID, ProyectDetailID }).FirstOrDefault();

                return model;
            }
        }
        public tblProyectLikes getByUserIDAndProyectDetailsID(int UserID, int ProyectDetailID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"
                                SELECT 
                                    *
                                  FROM tblProyectLikes
                                  WHERE
                                      tblProyectLikes.Active = 1 AND
                                      tblProyectLikes.UserID = @UserID AND
                                      tblProyectLikes.ProyectDetailID = @ProyectDetailID 
                                ";

                var model = dbConnection.Query<tblProyectLikes>(query, new { UserID, ProyectDetailID }).FirstOrDefault();

                return model;
            }
        }
    }
}
