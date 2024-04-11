using Antlr.Runtime.Misc;
using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.Filters
{
    public class fltUsers :BaseFilters
    {
        [Display(Name="ID")]
        public int UserID { get; set; }
        [Display(Name="Nombre")]
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int RolID { get; set; }
        [Display(Name = "Rol")]
        public string RolName { get; set; }
        [Display(Name = "Correo")]
        public string Email { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        public bool Active { get; set; }
        //only sorting

        public string Sort_value { get { return string.IsNullOrEmpty(SortingOrder) ? "" : SortingOrder; } set { } }
        public string UserID_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(UserID)} desc") ? $"{TableName}.{nameof(UserID)} asc" : $"{TableName}.{nameof(UserID)} desc"; } set { } }
        public string Name_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Name)} desc") ? $"{TableName}.{nameof(Name)} asc" : $"{TableName}.{nameof(Name)} desc"; } set { } }
        public string UserName_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(UserName)} desc") ? $"{TableName}.{nameof(UserName)} asc" : $"{TableName}.{nameof(UserName)} desc"; } set { } }
        public string Email_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Email)} desc") ? $"{TableName}.{nameof(Email)} asc" : $"{TableName}.{nameof(Email)} desc"; } set { } }
        public string RolID_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(RolID)} desc") ? $"{TableName}.{nameof(RolID)} asc" : $"{TableName}.{nameof(RolID)} desc"; } set { } }
        public string Active_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Active)} desc") ? $"{TableName}.{nameof(Active)} asc" : $"{TableName}.{nameof(Active)} desc"; } set { } }

        //Defaults
        public string TableName { get { return nameof(tblUsers); } set { } }
        public string dfSorting { get { return $"{TableName}.{nameof(UserID)}"; } set { } }

    }
}
