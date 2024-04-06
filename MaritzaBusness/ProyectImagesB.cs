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
    public class ProyectImagesB : BaseBusnessB
    {
        public Response Create(tblProyectImages model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectImages.Attach(model);
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
        public Response Update(tblProyectImages model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectImages.Attach(model);
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

        public List<tblProyectImages> GetListByProyectID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblProyectImages WHERE Active = 1 AND IsMainImage = 0 AND ProyectID = @ID ";
                var model = dbConnection.Query<tblProyectImages>(Query, new { ID }).ToList();
                return model;
            }
        }
        public tblProyectImages GetMainImageByProyectID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"SELECT * FROM tblProyectImages WHERE Active = 1 AND IsMainImage = 1 AND ProyectID = @ID ";
                var model = dbConnection.Query<tblProyectImages>(Query, new { ID }).FirstOrDefault();
                return model;
            }
        }

        public List<tblProyectImages> GetListMainImages()
        {
            using (IDbConnection dbConnection = new SqlConnection())
            {
                string Query = @"SELECT * FROM tblProyectImages WHERE Active = 1 AND IsMainImage = 1";
                var model = dbConnection.Query<tblProyectImages>(Query).ToList();
                return model;
            }
        }
    }
}
