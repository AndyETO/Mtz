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
    public class ProyectDetailsB : BaseBusnessB
    {
        public Response Create(tblProyectDetails model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectDetails.Attach(model);
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
        public Response Update(tblProyectDetails model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectDetails.Attach(model);
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


        public bool CheckIfExistsByProyectID(int ProyectID)
        {

            using (IDbConnection Connection = new SqlConnection(connection))
            {
                string Query = @"
                IF EXISTS(
                    SELECT ProyectDetailID
                    FROM tblProyectDetails
                    WHERE
                    tblProyectDetails.Active = 1 AND
                    tblProyectDetails.ProyectID = @ProyectID
                )
                    SELECT 'TRUE'
                  ELSE
                    SELECT 'FALSE'";
                var value = Connection.Query<bool>(Query, new { ProyectID }).FirstOrDefault();
                return value;
            }

        }

    }
}
