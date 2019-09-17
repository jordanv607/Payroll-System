using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class ReporteViewModel
    {
        [Display(Name = "Cédula del Empleado")]
        public string idEmpleado { get; set; }

        [Display(Name = "Fecha del pago")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable< System.DateTime> fechaPago { get; set; }
    }
}