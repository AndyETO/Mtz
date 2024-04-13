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
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProyectID { get; set; }
        [Required]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Presupuesto")]
        public decimal? Budget { get; set; }
        [Display(Name = "Genero")]
        public int? GenderID { get; set; }
        [Display(Name = "Tematica")]
        public int? TopicID { get; set; }
        [Display(Name = "Tipo de proyecto")]
        public int? ProyectTypeID { get; set; }

        [NotMapped] public List<tblPublishes> lstModelPublish { get; set; }
        [NotMapped] public tblPublishes MainPagePublish { get; set; }
        [NotMapped] public tblPublishes ProyectPublish { get; set; }

        [NotMapped] public int PrirityMainPage { get; set; }
        [NotMapped] public int PrirityProyects { get; set; }


        [NotMapped] public string StartDateMainPage { get; set; }
        [NotMapped] public string StartDateProyects { get; set; }
        [NotMapped] public string EndDateMainPage { get; set; }
        [NotMapped] public string EndDateProyects { get; set; }


        [NotMapped] public string CategoryName { get; set; }
        [NotMapped] public string GenderName { get; set; }
        [NotMapped] public string TopicName { get; set; }
        [NotMapped] public string ProyectTypeName { get; set; }
        //[NotMapped] public HttpPostedFileBase ImageBase { get; set; }
        //[NotMapped] public List<HttpPostedFileBase> ImageBase { get; set; }
        [NotMapped] public List<tblProyectImages> ProyectImagesMainImage { get; set; }
        [NotMapped] public tblProyectImages MainImage { get; set; }
        [NotMapped] public string MainImageBase { get; set; }
        [NotMapped] public List<tblProyectImages> lstModelProyectImages { get; set; }
        [NotMapped] public List<string> ImageBase { get; set; }
        [NotMapped] public List<tblProyectCharacteristics> lstModelProyectCharacteristics { get; set; }
        [NotMapped] public List<string> lstCharacteristics { get; set; }
        [NotMapped] public List<string> lstDeletedCharacteristics { get; set; }
    }
}
