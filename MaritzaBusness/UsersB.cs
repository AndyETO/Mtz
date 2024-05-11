using Dapper;
using MaritzaBusness.Base;
using MaritzaData;
using MaritzaData.ConfigClasses;
using MaritzaData.DTO;
using MaritzaData.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaBusness
{
    public class UsersB : BaseBusnessB
    {
        public StaticPagedList<dtoUsers> GetAll(fltUsers filter)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string columns = $" ,{filter.TableName}.{nameof(filter.UserID)}, {filter.TableName}.{nameof(filter.Name)},  {filter.TableName}.{nameof(filter.UserName)},  {filter.TableName}.{nameof(filter.Email)},  {filter.TableName}.{nameof(filter.Active)}";
                columns += $" ,{nameof(tblRoles)}.{nameof(tblRoles.Name)} AS {nameof(filter.RolName)}";
                List<object> fields = new List<object>() { nameof(filter.UserID), nameof(filter.Name), nameof(filter.UserName), nameof(filter.Email), nameof(filter.RolID), nameof(filter.Active) };
                List<object> values = new List<object>() { filter.UserID, filter.Name, filter.UserName, filter.Email, filter.RolID, 1 };
                string joins = $"LEFT JOIN {nameof(tblRoles)} ON {nameof(tblRoles)}.{nameof(tblRoles.RolID)} = {filter.TableName}.{nameof(filter.RolID)}";
                string conditions = GenereteConditions(filter.TableName, fields, values);
                filter.SortingOrder = string.IsNullOrEmpty(filter.SortingOrder) ? filter.dfSorting : filter.SortingOrder;
                string sQuery = GenereteQuery(filter.TableName, filter.SortingOrder, columns, joins, conditions, filter.PageNumber, filter.pageSize);
                List<dtoUsers> model = dbConnection.Query<dtoUsers>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoUsers>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }
        public tblUsers getById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"
                        SELECT 
                            tblUsers.*, 
                            tblRoles.Name AS RoleName 
                        FROM 
                        tblUsers 
                            LEFT JOIN tblRoles ON tblRoles.RolID = tblUsers.RolID
                        WHERE 
                            tblUsers.UserID = @id AND 
                            tblUsers.Active = 1";
                var model = dbConnection.Query<tblUsers>(query, new { id }).FirstOrDefault();
                return model;
            }
        }
        public List<dtoUsers> getList()
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"
                        SELECT 
                            tblUsers.*, 
                            tblRoles.Name AS RoleName 
                        FROM 
                        tblUsers 
                            LEFT JOIN tblRoles ON tblRoles.RolID = tblUsers.RolID
                        WHERE 
                            tblUsers.Active = 1";
                var model = dbConnection.Query<dtoUsers>(query).ToList();
                return model;
            }
        }
        public string getPasswordById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"
                        SELECT 
                            tblUsers.Password
                        FROM tblUsers 
                        WHERE 
                            tblUsers.UserID = @id AND 
                            tblUsers.Active = 1";
                var value = dbConnection.Query<string>(query, new { id }).FirstOrDefault();
                return value;
            }
        }
        public Response Create(tblUsers model)
        {
            Response response = new Response();

            try
            {
                db.tblUsers.Attach(model);
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

        public Response Delete(tblUsers model)
        {
            Response response = new Response();
            try
            {
                db.tblUsers.Attach(model);
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

        public Response Update(tblUsers model)
        {

            Response response = new Response();
            try
            {
                db.tblUsers.Attach(model);
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

        //GET'S

        public tblUsers getUserByUserName(string UserName)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                var model = dbConnection.QueryFirstOrDefault<tblUsers>("getUserByUserName", new { UserName }, commandType: CommandType.StoredProcedure);
                return model;
            }
        }

        public bool CheckIfExistsEmail(string Email)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"IF(Exists(SELECT tblUsers.Email FROM tblUsers WHERE Email = @Email))
                                    SELECT 'TRUE'
                                ELSE
                                    SELECT 'FALSE'";

                var model = dbConnection.Query<bool>(query, new { Email }).FirstOrDefault();

                return model;
            }
        }
        public bool CheckIfExistsUserName(string UserName)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"IF(Exists(SELECT tblUsers.UserName FROM tblUsers WHERE Active = 1 AND UserName = @UserName))
                                    SELECT 'TRUE'
                                ELSE
                                    SELECT 'FALSE'";

                var model = dbConnection.Query<bool>(query, new { UserName }).FirstOrDefault();

                return model;
            }
        }
        public bool CheckIfExistsByID(int ID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"IF(Exists(SELECT tblUsers.UserName FROM tblUsers WHERE Active = 1 AND UserID = @ID))
                                    SELECT 'TRUE'
                                ELSE
                                    SELECT 'FALSE'";

                var model = dbConnection.Query<bool>(query, new { ID }).FirstOrDefault();

                return model;
            }
        }
    }
}
