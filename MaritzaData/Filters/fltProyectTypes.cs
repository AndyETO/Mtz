using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaritzaData.Filters
{
    public class fltProyectTypes : BaseFilters
    {
        [Display(Name = "ID")]
        public int ProyectTypeID { get; set; }
        [Display(Name = "Tipo de proyecto")]
        public string Name { get; set; }
        public bool Active { get; set; }

        //only sorting

        public string Sort_value { get { return string.IsNullOrEmpty(SortingOrder) ? "" : SortingOrder; } set { } }


        public string ProyectTypeID_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(ProyectTypeID)} desc") ? $"{TableName}.{nameof(ProyectTypeID)} asc" : $"{TableName}.{nameof(ProyectTypeID)} desc"; } set { } }
        public string Name_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Name)} desc") ? $"{TableName}.{nameof(Name)} asc" : $"{TableName}.{nameof(Name)} desc"; } set { } }
        public string Active_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Active)} desc") ? $"{TableName}.{nameof(Active)} asc" : $"{TableName}.{nameof(Active)} desc"; } set { } }

        //Defaults
        public string TableName { get { return nameof(tblProyectTypes); } set { } }
        public string dfSorting { get { return $"{TableName}.{nameof(ProyectTypeID)}"; } set { } }
    }
}
