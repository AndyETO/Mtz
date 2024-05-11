using MaritzaData.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData
{
    public class tblCommentsLikes :tblBase
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentLikeID { get; set; }
        [Display(Name = "Like")]
        public bool? IsLiked { get; set; }
        [Display(Name = "User ID")]
        public int UserID { get; set; }
        public int ProyectCommentID { get; set; }
    }
}
