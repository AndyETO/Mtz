using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.DTO
{
    public class dtoLogin
    {
        [Display(Name="Usuario")]
        [Required(ErrorMessage ="Este campo es requerido.")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage ="Este campo es requerido.")]
        public string Password { get; set; }

        [NotMapped]
        public string RecoverPassword { get; set; }

    }
}
