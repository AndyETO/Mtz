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
    public class CharacteristicsB : BaseBusnessB
    {

        public Response Create(tblCharacteristics model)
        {
            Response response = new Response();
            try
            {
                db.tblCharacteristics.Attach(model);
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
        public Response Delete(tblCharacteristics model)
        {
            Response response = new Response();
            try
            {
                db.tblCharacteristics.Attach(model);
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
        public Response Update(tblCharacteristics model)
        {

            Response response = new Response();
            try
            {
                db.tblCharacteristics.Attach(model);
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
        public StaticPagedList<dtoCharacteristics> GetAll(fltCharacteristics filter)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string joins = "";
                string columns = $" ,{filter.TableName}.{nameof(filter.CharacteristicID)}, {filter.TableName}.{nameof(filter.Name)},  {filter.TableName}.{nameof(filter.Active)}";
                List<object> fields = new List<object>() { nameof(filter.CharacteristicID), nameof(filter.Name), nameof(filter.Active) };
                List<object> values = new List<object>() { filter.CharacteristicID, filter.Name, 1 };
                string conditions = GenereteConditions(filter.TableName, fields, values);
                filter.SortingOrder = string.IsNullOrEmpty(filter.SortingOrder) ? filter.dfSorting : filter.SortingOrder;
                string sQuery = GenereteQuery(filter.TableName, filter.SortingOrder, columns, joins, conditions, filter.PageNumber, filter.pageSize);
                List<dtoCharacteristics> model = dbConnection.Query<dtoCharacteristics>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoCharacteristics>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }
        public tblCharacteristics getById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = "SELECT * FROM tblCharacteristics WHERE Active = 1 AND CharacteristicID = @id";
                var model = dbConnection.Query<tblCharacteristics>(query, new { id }).FirstOrDefault();
                return model;
            }
        }
        public tblCharacteristics GetByName(string Name)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = "SELECT * FROM tblCharacteristics WHERE Active = 1 AND [Name] like @Name ";
                var model = dbConnection.Query<tblCharacteristics>(query, new { Name }).FirstOrDefault();
                return model;
            }
        }
        public List<tblCharacteristics> GetTop10ByName(string name, string notIncludeIds)
        {

            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string notIncludeiNCondition = string.Empty;
                if (!string.IsNullOrEmpty(notIncludeIds))
                    notIncludeiNCondition = $" AND CharacteristicID NOT IN ({notIncludeIds}) ";
                string Query = $"SELECT TOP (10) * FROM tblCharacteristics WHERE Active = 1 AND [Name] like '%{name}%' {notIncludeiNCondition}";
                var model = dbConnection.Query<tblCharacteristics>(Query).ToList();
                return model;
            }

        }

        public bool CheckIfExistsCharacteristicByID(int ID)
        {

            using (IDbConnection Connection = new SqlConnection(connection))
            {
                string Query = @"
                IF EXISTS(SELECT CharacteristicID FROM tblCharacteristics WHERE CharacteristicID = @ID)
                    SELECT 'TRUE'
                  ELSE
                    SELECT 'FALSE'";
                var value = Connection.Query<bool>(Query, new { ID }).FirstOrDefault();
                return value;
            }

        }

       
    }
}
