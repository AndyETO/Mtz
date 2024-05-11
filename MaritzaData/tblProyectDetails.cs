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
    public class tblProyectDetails : tblBase
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProyectDetailID { get; set; }
        [Display(Name = "Total de likes")]
        public int Likes { get; set; }
        [Display(Name = "Total de dislikes")]
        public int Dislikes { get; set; }
        [Display(Name = "Total comentarios")]
        public int NoComments { get; set; }
        [Display(Name = "Vistas")]
        public bool NoWatched { get; set; }
        public int ProyectID { get; set; }

    }
}
