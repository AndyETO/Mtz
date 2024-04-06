using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.Base
{
    public class MaritzaDB : DbContext
    {
        public MaritzaDB()
            : base("DefaultConnection")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }



        public DbSet<tblUsers> tblUsers { get; set; }
        public DbSet<tblProyects> tblProyects { get; set; }
        public DbSet<tblProyectTypes> tblProyectTypes { get; set; }
        public DbSet<tblTopics> tblTopics { get; set; }
        public DbSet<tblCharacteristics> tblCharacteristics { get; set; }
        public DbSet<tblProyectImages> tblProyectImages { get; set; }
        public DbSet<tblProyectCharacteristics> tblProyectCharacteristics { get; set; }
        public DbSet<tblPublishes> tblPublishes { get; set; }


        //metodos para obtener tabla 
        //public TOutput FunctionTableValue<TOutput>(string functionName, SqlParameter[] parameters)
        //{
        //    parameters = parameters ?? new SqlParameter[] { };

        //    string commandText = String.Format("exec {0}", String.Format("{0} {1}", functionName, String.Join(",", parameters.Select(x => x.ParameterName))));

        //    return ObjectContext.ExecuteStoreQuery<TOutput>(commandText, parameters[0], parameters[1], parameters[2]).FirstOrDefault();
        //}
        //private ObjectContext ObjectContext
        //{
        //    get { return (this as IObjectContextAdapter).ObjectContext; }
        //}
    }
}
