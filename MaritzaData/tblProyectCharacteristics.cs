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
    public class tblProyectCharacteristics 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int ProyectCharacteristicID { get; set; }
        public int ProyectID { get; set; }
        public int CharacteristicID { get; set; }
        [Display(Name = "Activo")]
        public bool Active { get; set; }
        [Display(Name = "Creado por")]
        public int CreatedBy { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Eliminado por")]
        public int DeletedBy { get; set; }
        [Display(Name = "Fecha de eliminación")]
        public DateTime? DeletedDate { get; set; }

        [NotMapped] public string CharacteristicName { get; set; }
    }
}
