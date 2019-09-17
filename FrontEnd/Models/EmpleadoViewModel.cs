using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class EmpleadoViewModel
    {
        [Display(Name = "Número colaborador")]
        [Key]
        public int idEmpleado { get; set; }

        [Display(Name = "Cédula")]
        public int idPersona { get; set; }

        public IEnumerable<PERSONAS> empleados { get; set; }
        public PERSONAS empleado { get; set; }

        [Display(Name = "Departamento")]
        public int departamento { get; set; }
        public IEnumerable<PARAMETROS> departamentos { get; set; }
        [Display(Name = "Departamento")]
        public PARAMETROS depto { get; set; }

        [Display(Name = "Puesto")]
        public int puesto { get; set; }
        public IEnumerable<PARAMETROS> puestos { get; set; }
        public PARAMETROS ocupacion { get; set; }

        [Display(Name = "Salario mensual")]
        public decimal salarioMensual { get; set; }

        [Display(Name = "Utiliza arma")]
        public int portacionArma { get; set; }
        public IEnumerable<PARAMETROS> portacionArmas { get; set; }
        public PARAMETROS portaArma { get; set; }

        [Display(Name = "Vencimiento permiso (arma)")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> vencimientoPermisoArma { get; set; }

        [Display(Name = "Cuenta bancaria")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string numeroCuentaBancaria { get; set; }
    }
}