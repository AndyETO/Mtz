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
        public int ProyectID { get; set; }
        public string Title { get; set; }

    }
}
