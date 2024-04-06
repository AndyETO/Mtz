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
    public class TopicsB : BaseBusnessB
    {

        public Response Create(tblTopics model)
        {
            Response response = new Response();
            try
            {
                db.tblTopics.Attach(model);
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
        public Response Delete(tblTopics model)
        {
            Response response = new Response();
            try
            {
                db.tblTopics.Attach(model);
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
        public Response Update(tblTopics model)
        {

            Response response = new Response();
            try
            {
                db.tblTopics.Attach(model);
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
        public StaticPagedList<dtoTopics> GetAll(fltTopics filter)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string joins = "";
                string columns = $" ,{filter.TableName}.{nameof(filter.TopicID)}, {filter.TableName}.{nameof(filter.Name)},  {filter.TableName}.{nameof(filter.Active)}";
                List<object> fields = new List<object>() { nameof(filter.TopicID), nameof(filter.Name), nameof(filter.Active) };
                List<object> values = new List<object>() { filter.TopicID, filter.Name, 1 };
                string conditions = GenereteConditions(filter.TableName, fields, values);
                filter.SortingOrder = string.IsNullOrEmpty(filter.SortingOrder) ? filter.dfSorting : filter.SortingOrder;
                string sQuery = GenereteQuery(filter.TableName, filter.SortingOrder, columns, joins, conditions, filter.PageNumber, filter.pageSize);
                List<dtoTopics> model = dbConnection.Query<dtoTopics>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoTopics>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }
        public tblTopics getById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = "SELECT * FROM tblTopics WHERE TopicID = @id";
                var model = dbConnection.Query<tblTopics>(query, new { id }).FirstOrDefault();
                return model;
            }
        }


        public List<tblTopics> GetList()
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = "SELECT * FROM tblTopics WHERE Active = 1";
                var model = dbConnection.Query<tblTopics>(Query).ToList();
                return model;
            }
        }

    }
}
