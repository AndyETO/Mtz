using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData
{
    public class tblGenders
    {
        [Key] 
        public int GenderID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
