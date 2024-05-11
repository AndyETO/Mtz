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
using MaritzaData.DTO;
using Dapper;

namespace MaritzaBusness
{
    public class ProyectCommentsB : BaseBusnessB
    {
        public Response Create(tblProyectComments model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectComments.Attach(model);
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                response.Result = Result.Ok;
            }
            catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }
        public Response Update(tblProyectComments model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectComments.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                response.Result = Result.Ok;
            }
            catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }

        public List<dtoProyectComments> getListCommentsByProyectID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblProyectComments WHERE Active = 1 AND ProyectID = @ID ORDER BY CreatedDate DESC";
                var model = dbConnection.Query<dtoProyectComments>(Query, new { ID }).ToList();
                return model;
            }
        }
        public List<dtoProyectComments> getListCommentsByProyectIDNoanswers(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"
                            SELECT 
                                tblProyectComments.ProyectCommentID
                                  ,tblProyectComments.UserID
                                  ,tblProyectComments.Comment
                                  ,tblProyectComments.IsValidated
                                  ,tblProyectComments.ValidatedBy
                                  ,tblProyectComments.ValidateDate
                                  ,tblProyectComments.IsAnswer
                                  ,tblProyectComments.ProyectID
	                              ,COUNT(AnswerComment.Active) AS NoAnswers
                            FROM tblProyectComments 
                            LEFT JOIN tblProyectComments AS AnswerComment ON AnswerComment.sProyectCommentID = tblProyectComments .ProyectCommentID
                            WHERE tblProyectComments.Active = 1 AND tblProyectComments.IsAnswer = 0 AND tblProyectComments.ProyectID = 3043 
                            GROUP BY tblProyectComments.[ProyectCommentID]
                                  ,tblProyectComments.UserID
                                  ,tblProyectComments.Comment
                                  ,tblProyectComments.IsValidated
                                  ,tblProyectComments.ValidatedBy
                                  ,tblProyectComments.ValidateDate
                                  ,tblProyectComments.IsAnswer
                                  ,tblProyectComments.ProyectID
	                              ,tblProyectComments.CreatedDate 
                            ORDER BY tblProyectComments.CreatedDate DESC";
                var model = dbConnection.Query<dtoProyectComments>(Query, new { ID }).ToList();
                return model;
            }
        }
        public List<dtoProyectComments> getListAnswerCommentsByProyectCommentID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblProyectComments WHERE Active = 1 AND IsAnswer = 0 AND sProyectCommentID = @ID ORDER BY CreatedDate DESC";
                var model = dbConnection.Query<dtoProyectComments>(Query, new { ID }).ToList();
                return model;
            }
        }
    }
}
