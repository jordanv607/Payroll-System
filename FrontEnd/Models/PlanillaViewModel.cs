using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class PlanillaViewModel
    {
        [Display(Name = "Cédula")]
        public string numeroIdentificacion { get; set; }

        [Display(Name = "Nombre")]
        public string nombrePersona { get; set; }

        [Display(Name = "Primer apellido")]
        public string primerApellido { get; set; }

        [Display(Name = "Segundo apellido")]
        public string segundoApellido { get; set; }

        [Display(Name = "Correo electrónico")]
        public string correoElectronico { get; set; }

        [Display(Name = "Cuenta Bancaria")]
        public string numeroCuentaBancaria { get; set; }

        [Display(Name = "Salario Neto")]
        [DisplayFormat(DataFormatString = "₡{0:#,0}", ApplyFormatInEditMode = true)]
        public decimal salarioNeto { get; set; }

        [Display(Name = "Deducciones")]
        [DisplayFormat(DataFormatString = "₡{0:#,0}", ApplyFormatInEditMode = true)]
        public decimal totalDeducciones { get; set; }

        [Display(Name = "Salario Bruto")]
        [DisplayFormat(DataFormatString = "₡{0:#,0}", ApplyFormatInEditMode = true)]
        public decimal salarioBruto { get; set; }

        [Display(Name = "Fecha del pago")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime fechaPago { get; set; }
    }
}