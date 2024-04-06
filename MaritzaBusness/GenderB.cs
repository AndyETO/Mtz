using Dapper;
using MaritzaBusness.Base;
using MaritzaData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MaritzaBusness
{
    public class GenderB : BaseBusnessB
    {

        public List<tblGenders> GetList()
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = "SELECT * FROM tblGenders WHERE Active = 1";
                var model = dbConnection.Query<tblGenders>(Query).ToList();
                return model;
            }
        }

        public tblGenders GetByID(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = "SELECT * FROM tblGender WHERE Active = 1 AND GenderID = @id";
                var model = dbConnection.Query<tblGenders>(Query).FirstOrDefault();
                return model;
            }
        }
    }
}
