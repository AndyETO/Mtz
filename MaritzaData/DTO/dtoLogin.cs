using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.DTO
{
    public class dtoLogin
    {
        [Display(Name="Nombre de usuario")]
        [Required(ErrorMessage ="Este campo es requerido.")]
        public int UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage ="Este campo es requerido.")]
        public int Password { get; set; }

        
    }
}
