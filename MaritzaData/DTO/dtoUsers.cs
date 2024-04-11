using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.DTO
{
    public class dtoUsers: dtoBase
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int RolID { get; set; }
        public string RolName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
