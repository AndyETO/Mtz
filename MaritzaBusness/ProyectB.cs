using Dapper;
using MaritzaBusness.Base;
using MaritzaData;
using MaritzaData.Filters;

using MaritzaData.ConfigClasses;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaritzaData.DTO;

namespace MaritzaBusness
{
    public class ProyectB : BaseBusnessB
    {

        public Response Create(tblProyects model)
        {
            Response response = new Response();

            try
            {
                db.tblProyects.Attach(model);
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
        public Response Delete(tblProyects model)
        {
            Response response = new Response();
            try
            {
                db.tblProyects.Attach(model);
                db.Entry(model).State = EntityState.Deleted;
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
        public Response Update(tblProyects model)
        {

            Response response = new Response();
            try
            {
                db.tblProyects.Attach(model);
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

        public StaticPagedList<dtoProyects> GetAll(fltProyects filter)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                //string query = "select * from tblProyects";

                //string query = @"
                //SELECT * FROM ( 
                //    SELECT ROW_NUMBER() OVER 
                //    (ORDER BY tblProyects.[ProyectID] ASC) AS ROWNUM,
                //    Count(*) over() AS TotalCount
                //     ,tblProyects.[ProyectID]
                //        ,tblProyects.[Title]
                //        ,tblProyects.[Description]
                //        ,tblProyects.[Comment]
                //    FROM tblProyects  
                //    WHERE 
                //    Active = 1
                //) AS RESULT WHERE ROWNUM BETWEEN ((1 - 1) * 10 + 1) AND (1 *10) ORDER BY  ROWNUM ASC
                //";

                string query = @"
                SELECT * FROM ( 
                    SELECT ROW_NUMBER() OVER 
                    (ORDER BY tblProyects.[ProyectID] ASC) AS ROWNUM,
                    Count(*) over() AS TotalCount
	                    ,tblProyects.[ProyectID]
                        ,tblProyects.[Title]
                        ,tblProyects.[Description]
                        ,tblProyects.[Comment]
                    FROM tblProyects  
                    WHERE 
                    Active = 1
                ) AS RESULT WHERE ROWNUM BETWEEN ((" + filter.PageNumber + @" - 1) * 10 + 1) AND (" + filter.PageNumber + @" *10) ORDER BY  ROWNUM ASC
                ";

                //GENERE QUERY

                //Table

                //Atributes

                //Joins

                //Conditions -- filters

                //
                List<dtoProyects> model = dbConnection.Query<dtoProyects>(query).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoProyects>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }

        public tblProyects getById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = "Select * from tblProyects where ProyectID = @id";

                var model = dbConnection.Query<tblProyects>(query, new { id }).FirstOrDefault();

                return model;
            }
        }
    }
}
