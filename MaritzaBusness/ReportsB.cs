using MaritzaBusness.Base;
using MaritzaData.DTO;
using MaritzaData.Filters;
using MaritzaData;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MaritzaBusness
{
    public class ReportsB : BaseBusnessB
    {
        public StaticPagedList<dtoUsersRpts> GetAllUsersRpt(fltUsers filter)
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
                List<dtoUsersRpts> model = dbConnection.Query<dtoUsersRpts>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoUsersRpts>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }
        }
    }
}
