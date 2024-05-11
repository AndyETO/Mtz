using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.DTO
{
    public class dtoUsersRpts :dtoBase
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RolName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedName { get; set; }
        public int ValidatedComments { get; set; }
        public int PublishedProyects { get; set; }
        public int CreatedProyects { get; set; }
    }
}
