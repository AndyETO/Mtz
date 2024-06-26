﻿using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.Filters
{
    public class fltSearch : BaseFilters
    {
        public int ProyectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> lstCharacteristicsIDs { get; set; }
        public decimal? MinBudget { get; set; }
        public decimal? MaxBudget { get; set; }
        public bool Active { get; set; }

        public string SearchTerm { get; set; }
        public int TopicID { get; set; }
        public int ProyectTypeID { get; set; }
        public int GenderID { get; set; }




        //only sorting

        public string Sort_value { get { return string.IsNullOrEmpty(SortingOrder) ? "" : SortingOrder; } set { } }
        public string ProyectID_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(ProyectID)} desc") ? $"{TableName}.{nameof(ProyectID)} asc" : $"{TableName}.{nameof(ProyectID)} desc"; } set { } }
        public string Title_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Title)} desc") ? $"{TableName}.{nameof(Title)} asc" : $"{TableName}.{nameof(Title)} desc"; } set { } }
        public string Description_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Description)} desc") ? $"{TableName}.{nameof(Description)} asc" : $"{TableName}.{nameof(Description)} desc"; } set { } }
        public string Active_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Active)} desc") ? $"{TableName}.{nameof(Active)} asc" : $"{TableName}.{nameof(Active)} desc"; } set { } }

        //Defaults
        public string TableName { get { return nameof(tblProyects); } set { } }
        public string dfSorting { get { return $"{TableName}.{nameof(ProyectID)}"; } set { } }

    }
}
