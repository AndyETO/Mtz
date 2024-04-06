using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData
{
    public class tblPublishes : tblBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublishID { get; set; }
        public int ProyectID { get; set; }
        public int PublishTypeID { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PriorityLevel { get; set; }
    }
}
