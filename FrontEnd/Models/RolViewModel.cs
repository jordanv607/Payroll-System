using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class RolViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int idRol { get; set; }
        [Display(Name = "Nombre Rol")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string nombreRol { get; set; }
        [Display(Name = "Descripción")]
        public string descripcionRol { get; set; }
    }
}