using MaritzaData.Base;
using MaritzaData.ConfigClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData
{
    public class tblUsers :tblBase
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Display(Name="Nombre")]
        [Required(ErrorMessage ="Este elemento es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "Este elemento es requerido.")]
        public string UserName { get; set; }
        [Display(Name = "RolID")]
        [Required(ErrorMessage = "Este elemento es requerido.")]
        public bool RolID { get; set; }

        [Display(Name = "Apellido")]
        //[Required(ErrorMessage = "Este elemento es requerido.")]
        public string LastName { get; set; }
        [Display(Name="Correo")]
        [Required(ErrorMessage ="Este elemento es requerido.")]
        public string Email { get; set; }
        [Display(Name="Celular")]
        [Required(ErrorMessage ="Este elemento es requerido.")]
        public string Phone { get; set; }
        [Display(Name="Contraseña")]
        [Required(ErrorMessage ="Este elemento es requerido.")]
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        [Display(Name = "Recordar contraseña")]
        public bool RememberPassword { get; set; }
        [NotMapped]
        [Display(Name = "Rol")]
        public string RoleName { get; set; }

    }
}
