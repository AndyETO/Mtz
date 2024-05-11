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
    public class tblProyectComments : tblBase
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProyectCommentID { get; set; }
        [Display(Name = "User ID")]
        public int UserID { get; set; }
        [Display(Name ="Comentario")]
        public string Comment { get; set; }
        [Display(Name = "Validado")]
        public bool IsValidated { get; set; }
        [Display(Name = "Validado por")]
        public int ValidatedBy { get; set; }
        [Display(Name = "Fecha de validación")]
        public DateTime? ValidateDate { get; set; }
        [Display(Name = "Respuesta ID")]
        public int sProyectCommentID { get; set; }
        [Display(Name = "Respuesta")]
        public bool IsAnswer { get; set; }
        [Display(Name = "Proyecto ID")]
        public int ProyectID { get; set; }

    }
}
