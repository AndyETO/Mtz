using Dapper;
using MaritzaBusness.Base;
using MaritzaData;
using MaritzaData.ConfigClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaBusness
{
    public class PublishesB : BaseBusnessB
    {
        public Response Create(tblPublishes model)
        {
            Response response = new Response();
            try
            {
                db.tblPublishes.Attach(model);
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
        public Response Update(tblPublishes model)
        {
            Response response = new Response();
            try
            {
                db.tblPublishes.Attach(model);
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

        public List<tblPublishes> GetListByProyectID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblPublishes WHERE Active = 1 AND ProyectID = @ID";
                var model = dbConnection.Query<tblPublishes>(Query, new { ID }).ToList();
                return model;
            }

        }
        public tblPublishes GetMainPageByProyectID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblPublishes WHERE Active = 1 AND PublishTypeID = 1 AND ProyectID = @ID";
                var lstModel = dbConnection.Query<tblPublishes>(Query, new { ID }).FirstOrDefault();
                return lstModel;
            }
        }
        public tblPublishes GetProyectByProyectID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblPublishes WHERE Active = 1 AND PublishTypeID = 2 AND ProyectID = @ID";
                var lstModel = dbConnection.Query<tblPublishes>(Query, new { ID }).FirstOrDefault();
                return lstModel;
            }
        }
    }
}
