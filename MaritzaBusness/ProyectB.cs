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


                //string query = @"
                //SELECT * FROM 
                // ( 
                // SELECT ROW_NUMBER() OVER (ORDER BY tblProyects.[ProyectID] ASC) AS ROWNUM,
                //  Count(*) over() AS TotalCount
                //     ,tblProyects.[ProyectID]
                //        ,tblProyects.[Title]
                //        ,tblProyects.[Description]
                //        ,tblProyects.[Comment]
                //    FROM tblProyects  
                //    WHERE 
                //    Active = 1
                //) AS RESULT WHERE ROWNUM BETWEEN ((" + filter.PageNumber + @" - 1) * 10 + 1) AND (" + filter.PageNumber + @" * 10) ORDER BY  ROWNUM ASC
                //";




                string joins = "";

                //Base columns
                string columns = $" ,{filter.TableName}.{nameof(filter.ProyectID)}, {filter.TableName}.{nameof(filter.Title)}, {filter.TableName}.{nameof(filter.Description)}, {filter.TableName}.{nameof(filter.Comment)}, {filter.TableName}.{nameof(filter.Active)}";

                //Base conditions
                List<object> fields = new List<object>() { nameof(filter.ProyectID), nameof(filter.Title), nameof(filter.Description), nameof(filter.Comment), nameof(filter.Active) };
                List<object> values = new List<object>() { filter.ProyectID, filter.Title, filter.Description, filter.Comment, 1 };
                string conditions = GenereteConditions(filter.TableName, fields, values);

                
                filter.SortingOrder = string.IsNullOrEmpty(filter.SortingOrder) ? filter.dfSorting : filter.SortingOrder;


                string sQuery = GenereteQuery(filter.TableName, filter.SortingOrder, columns, joins, conditions,  filter.PageNumber, filter.pageSize);


                List<dtoProyects> model = dbConnection.Query<dtoProyects>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoProyects>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }



        public tblProyects getById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = "SELECT * FROM tblProyects WHERE ProyectID = @id";

                var model = dbConnection.Query<tblProyects>(query, new { id }).FirstOrDefault();

                return model;
            }
        }

        public List<tblProyects> getRandomItems()
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"SELECT TOP 10 * from tblProyects WHERE Active = 1 AND IsFrontPage = 1 ORDER BY RAND()";

                var model = dbConnection.Query<tblProyects>(query).ToList();

                return model;
            }
        }
    }
}
