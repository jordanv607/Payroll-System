using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class UsuarioViewModel
    {
        [Display(Name = "Código usuario")]
        [Key]
        public int idUsuario { get; set; }
        [Display(Name = "Cédula")]
        public int idPersona { get; set; }
        public IEnumerable<PERSONAS> Personas { get; set; }
        public PERSONAS persona { get; set; }
        [Display(Name = "Nombre Rol")]
        public int idRol { get; set; }
        public IEnumerable<ROLES> Roles { get; set; }
        public ROLES Rol { get; set; }
        [Display(Name = "Nombre Usuario")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string usuario { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [DisplayFormat(DataFormatString = "●●●●")]
        [DataType(DataType.Password)]
        public string contrasenia { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}