using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class PersonaViewModel
    {
        [Display(Name = "Código persona")]
        [Key]
        public int idPersona { get; set; }
        [Display(Name = "Cédula")]
        public string numeroIdentificacion { get; set; }
        [Display(Name = "Nombre")]
        public string nombrePersona { get; set; }
        [Display(Name = "Primer apellido")]
        public string primerApellido { get; set; }
        [Display(Name = "Segundo apellido")]
        public string segundoApellido { get; set; }
        [Display(Name = "Genero")]
        public int genero { get; set; }
        public IEnumerable<PARAMETROS> generos { get; set; }
        [Display(Name = "Departamento")]
        public PARAMETROS generoObj { get; set; }

        [Display(Name = "Estado")]
        public int estado { get; set; }
        public IEnumerable<PARAMETROS> estados { get; set; }
        [Display(Name = "Departamento")]
        public PARAMETROS estadoObj { get; set; }

        [Display(Name = "Nacionalidad")]
        public int nacionalidad { get; set; }
        public IEnumerable<PARAMETROS> nacionalidades { get; set; }
        [Display(Name = "Departamento")]
        public PARAMETROS nacionalidadObj { get; set; }

        [Display(Name = "Correo electrónico")]
        public string correoElectronico { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Dirección")]
        public string direccionResidencia { get; set; }
    }
}