//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd.Entities
{
    using System;
    
    public partial class sp_getDatosPlanillaCedula_Result
    {
        public int idPago { get; set; }
        public int idEmpleado { get; set; }
        public int idPersona { get; set; }
        public string numeroIdentificacion { get; set; }
        public string nombre { get; set; }
        public System.DateTime fechaPago { get; set; }
        public decimal salarioBruto { get; set; }
        public decimal totalDeducciones { get; set; }
        public decimal salarioNeto { get; set; }
        public string numeroCuentaBancaria { get; set; }
    }
}