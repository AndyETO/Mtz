using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MaritzaData.Base
{
    public class AuxMethods
    {

        public string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public string GetUserRol(string UserID)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                var model = dbConnection.QueryFirstOrDefault<string>("GetUserRol", new { UserID }, commandType: CommandType.StoredProcedure);
                return model;
            }
        }
    }
}
