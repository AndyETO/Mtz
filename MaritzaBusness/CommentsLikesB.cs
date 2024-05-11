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
    public class CommentsLikesB : BaseBusnessB
    {

        public Response Create(tblCommentsLikes model)
        {
            Response response = new Response();
            try
            {
                db.tblCommentsLikes.Attach(model);
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
        public Response Update(tblCommentsLikes model)
        {
            Response response = new Response();
            try
            {
                db.tblCommentsLikes.Attach(model);
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


        public tblCommentsLikes getByUserIDAndProyectCommentID(int UserID, int ProyectCommentID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"
                                SELECT 
                                    *
                                  FROM 
                                  WHERE
                                      .Active = 1 AND
                                      .UserID = @UserID AND
                                      .ProyectDetailID = @ProyectCommentID 
                                ";

                var model = dbConnection.Query<tblCommentsLikes>(query, new { UserID, ProyectCommentID }).FirstOrDefault();

                return model;
            }
        }

    }
}
