using MaritzaData.Base;
using MaritzaData.ConfigClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MaritzaData
{
    public class tblProyects : tblBase
    {
        [Key]
        [Required]
        [Display(Name = "Identificador de proyecto")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProyectID { get; set; }
        [Required]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Comentario")]
        public string Comment { get; set; }
        //[Required]
        [Display(Name = "Imagen")]
        public string Image { get; set; }
        //[Required]
        //[Display(Name = "Categoría")]
        //public int CategoryID { get; set; }

        [NotMapped] public int CategoryName { get; set; }
        [NotMapped] public HttpPostedFileBase ImageBase { get; set; }
    }
}
