using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.Base
{
    public class tblBase
    {

        [Display(Name = "Activo")]
        public bool Active { get; set; }
        [Display(Name = "Creado por")]
        public int CreatedBy { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Actualizado por")]
        public int UpdatedBy { get; set; }
        [Display(Name = "Fecha de actualización")]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "Eliminado por")]
        public int DeletedBy { get; set; }
        [Display(Name = "Fecha de eliminación")]
        public DateTime? DeletedDate { get; set; }
        [NotMapped] public string CreatedByName { get; set; }
        [NotMapped] public string UpdatedByName { get; set; }

    }
}
