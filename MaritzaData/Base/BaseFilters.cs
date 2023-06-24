using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.Base
{
    public class BaseFilters
    {
        public string SortingOrder { get; set; }
        public int PageNumber { get; set; } =1;
        public int pageSize { get; set; } = 10;
        public int TotalIemCount{ get; set; } = 10;
    }
}
