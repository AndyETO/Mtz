using MaritzaBusness.Base;
using MaritzaData;
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
    public class RolesB : BaseBusnessB
    {
        public List<tblRoles> GetList()
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = "SELECT * FROM tblRoles WHERE Active = 1";
                var model = dbConnection.Query<tblRoles>(Query).ToList();
                return model;
            }
        }
    }
}
