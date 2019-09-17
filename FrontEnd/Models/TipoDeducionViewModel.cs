using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace FrontEnd.Models
{
    public class TipoDeducionViewModel
    {
       
        [Display(Name = "Código deducción")]
        [Key]
        public int idDeduccion { get; set; }
        [Display(Name = "Nombre")]
        public string nombreDeduccion { get; set; }
        [Display(Name = "Porcentaje")]
        public decimal porcentaje { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
    }
}