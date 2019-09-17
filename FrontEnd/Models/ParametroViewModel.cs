using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class ParametroViewModel

    {
        //
        [Display(Name = "Código parámetro")]
        [Key]
        public int idParametro { get; set; }

        [Display(Name = "Nombre tabla")]
        public string tabla { get; set; }

        [Display(Name = "Nombre campo")]
        public string campo { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
    }
}