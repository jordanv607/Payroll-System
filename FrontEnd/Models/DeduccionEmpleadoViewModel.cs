using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class DeduccionEmpleadoViewModel
    {
        [Display(Name = "Código deducción del empleado")]
        [Key]
        public int idDeduccionEmpleado { get; set; }

        [Display(Name = "Cédula del Empleado")]
        public int idEmpleado { get; set; }

        public IEnumerable<EMPLEADOS> EMPLEADOS { get; set; }
        public EMPLEADOS EMPLEADO{ get; set; }

        public IEnumerable<PERSONAS> Personas { get; set; }
        public PERSONAS PERSONA { get; set; }


        [Display(Name = "Deducción")]
        public string idDeduccion { get; set; }

        public IEnumerable<TIPO_DEDUCCIONES>  TipoDeduccion{ get; set; }
        public TIPO_DEDUCCIONES TIPO_DEDUCCION { get; set; }

        [Display(Name = "Monto")]
        public decimal monto { get; set; }

        [Display(Name = "Fecha del pago")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime fechaPago { get; set; }
    }
}