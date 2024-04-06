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
    public class fltProyects : BaseFilters
    {
        [Display(Name = "ID")]
        public int ProyectID { get; set; }
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }

        //only sorting

        public string Sort_value { get { return string.IsNullOrEmpty(SortingOrder) ? "" : SortingOrder; } set { } }


        public string ProyectID_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(ProyectID)} desc") ? $"{TableName}.{nameof(ProyectID)} asc" : $"{TableName}.{nameof(ProyectID)} desc"; } set { } }
        public string Title_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Title)} desc") ? $"{TableName}.{nameof(Title)} asc" : $"{TableName}.{nameof(Title)} desc"; } set { } }
        public string Description_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Description)} desc") ? $"{TableName}.{nameof(Description)} asc" : $"{TableName}.{nameof(Description)} desc"; } set { } }
        //public string Comment_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Comment)} desc") ? $"{TableName}.{nameof(Comment)} asc" : $"{TableName}.{nameof(Comment)} desc"; } set { } }
        public string Active_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Active)} desc") ? $"{TableName}.{nameof(Active)} asc" : $"{TableName}.{nameof(Active)} desc"; } set { } }

        //Defaults
        public string TableName { get { return nameof(tblProyects); } set { } }
        public string dfSorting { get { return $"{TableName}.{nameof(ProyectID)}"; } set { } }
    }
}
