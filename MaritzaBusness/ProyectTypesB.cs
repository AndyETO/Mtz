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
    public class ProyectTypesB : BaseBusnessB
    {

        public Response Create(tblProyectTypes model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectTypes.Attach(model);
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
        public Response Delete(tblProyectTypes model)
        {
            Response response = new Response();
            try
            {
                db.tblProyectTypes.Attach(model);
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
        public Response Update(tblProyectTypes model)
        {

            Response response = new Response();
            try
            {
                db.tblProyectTypes.Attach(model);
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
        public StaticPagedList<dtoProyectTypes> GetAll(fltProyectTypes filter)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string joins = "";
                string columns = $" ,{filter.TableName}.{nameof(filter.ProyectTypeID)}, {filter.TableName}.{nameof(filter.Name)},  {filter.TableName}.{nameof(filter.Active)}";
                List<object> fields = new List<object>() { nameof(filter.ProyectTypeID), nameof(filter.Name), nameof(filter.Active) };
                List<object> values = new List<object>() { filter.ProyectTypeID, filter.Name, 1 };
                string conditions = GenereteConditions(filter.TableName, fields, values);
                filter.SortingOrder = string.IsNullOrEmpty(filter.SortingOrder) ? filter.dfSorting : filter.SortingOrder;
                string sQuery = GenereteQuery(filter.TableName, filter.SortingOrder, columns, joins, conditions, filter.PageNumber, filter.pageSize);
                List<dtoProyectTypes> model = dbConnection.Query<dtoProyectTypes>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoProyectTypes>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }
        public tblProyectTypes getById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = "SELECT * FROM tblProyectTypes WHERE ProyectTypeID = @id";
                var model = dbConnection.Query<tblProyectTypes>(query, new { id }).FirstOrDefault();
                return model;
            }
        }



        public List<tblProyectTypes> GetList()
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = "SELECT * FROM tblProyectTypes WHERE Active = 1";
                var model = dbConnection.Query<tblProyectTypes>(Query).ToList();
                return model;
            }
        }
    }
}
