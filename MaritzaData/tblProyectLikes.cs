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
    public class tblProyectLikes : tblBase
    {
        [Key] 
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProyectLikesID { get; set; }
        [Display(Name = "Like")]
        public bool? IsLiked { get; set; }
        [Display(Name = "User ID")]
        public int UserID { get; set; }
        public int ProyectDetailID { get; set; }

    }
}
