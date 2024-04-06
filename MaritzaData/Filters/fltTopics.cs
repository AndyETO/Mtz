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
    public class fltTopics : BaseFilters
    {
        [Display(Name = "ID")]
        public int TopicID { get; set; }
        [Display(Name = "Tematica")]
        public string Name { get; set; }
        public bool Active { get; set; }

        //only sorting

        public string Sort_value { get { return string.IsNullOrEmpty(SortingOrder) ? "" : SortingOrder; } set { } }
        public string TopicID_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(TopicID)} desc") ? $"{TableName}.{nameof(TopicID)} asc" : $"{TableName}.{nameof(TopicID)} desc"; } set { } }
        public string Name_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Name)} desc") ? $"{TableName}.{nameof(Name)} asc" : $"{TableName}.{nameof(Name)} desc"; } set { } }
        public string Active_Sort { get { return Sort_value.Contains($"{TableName}.{nameof(Active)} desc") ? $"{TableName}.{nameof(Active)} asc" : $"{TableName}.{nameof(Active)} desc"; } set { } }

        //Defaults
        public string TableName { get { return nameof(tblTopics); } set { } }
        public string dfSorting { get { return $"{TableName}.{nameof(TopicID)}"; } set { } }
    }
}
