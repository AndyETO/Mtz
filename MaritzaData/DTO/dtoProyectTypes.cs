using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaritzaData.DTO
{
    public class dtoProyectTypes:dtoBase
    {
        public int ProyectTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
