using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaritzaData.Base;
namespace MaritzaBusness.Base
{
    public class BaseBusnessB
    {
        public string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public MaritzaDB db = new MaritzaDB();


        public string GenereteQuery(string table, string sorting, string columns, string joins, string conditions, int PageNumber, int pageSize)
        {
            string Query = "";
            Query += " SELECT * FROM ( ";
            Query += $" SELECT ROW_NUMBER() OVER (ORDER BY {sorting}) AS ROWNUM,";
            Query += " Count(*) over() AS TotalCount";
            Query += columns;
            Query += $" FROM {table}  ";
            Query += joins;
            if (!string.IsNullOrEmpty(conditions))
                Query += " WHERE ";
            Query += conditions;
            Query += ") AS RESULT WHERE ROWNUM BETWEEN (("+ PageNumber + " - 1) * " + pageSize + @") AND (" + PageNumber + @" * "+ pageSize + ") ORDER BY  ROWNUM ASC";
            return Query;
        }

        public string GenereteConditions(string table, List<object> columns, List<object> values)
        {
            string Condition = "";
            if (columns != null && values != null)
            {
                List<string> Numbers = new List<string>() { "System.Int64", "System.Int32", "System.Int16", "System.Double", "System.Single", "System.Decimal" };
                List<string> Strings = new List<string>() { "System.String", "System.Char" };
                string type = "";

                for (int i = 0; i < columns.Count; i++)
                {

                    var element = values[i];
                    if (element != null)
                        type = element.GetType().ToString();


                    if (!string.IsNullOrEmpty(type))
                    {
                        if (Strings.FindIndex(item => item.Equals(type)) != -1)
                        {
                            if (i != columns.Count - 1)
                                Condition += $" ('' like '%{values[i]}%' or {table}.{columns[i]} like '%{values[i]}%') AND ";
                            else
                                Condition += $" ('' like '%{values[i]}%' or {table}.{columns[i]} like '%{values[i]}%') ";
                        }
                        else if (Numbers.FindIndex(item => item.Equals(type)) != -1)
                        {
                            if (i != columns.Count - 1)
                                Condition += $" (0 = {values[i]} or {table}.{columns[i]} = {values[i]} ) AND ";
                            else
                                Condition += $" (0 = {values[i]} or {table}.{columns[i]} = {values[i]} ) ";
                        }
                        else if (element.Equals("System.Boolean"))
                        {
                            bool flag = (bool)values[i];
                            int newValue = flag ? 1 : 0;
                            Condition += $" (0 = {newValue} or {table}.{columns[i]} = {newValue} ) AND ";
                        }
                        type = "";
                    }






                }
            }



            return Condition;
        }
    }


}
