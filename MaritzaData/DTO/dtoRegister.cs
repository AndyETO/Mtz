using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.DTO
{
    public class dtoRegister 
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string UserName { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Password { get; set; }
        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Email { get; set; }
        
        
    }
}
