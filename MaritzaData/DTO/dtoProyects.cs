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
    public class dtoProyects:dtoBase
    {
        public int ProyectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
    }
}
