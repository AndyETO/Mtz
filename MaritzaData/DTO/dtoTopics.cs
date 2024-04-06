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
    public class dtoTopics:dtoBase
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
    }
}
