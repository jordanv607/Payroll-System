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
    using System.Collections.Generic;
    
    public partial class TIPO_DEDUCCIONES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_DEDUCCIONES()
        {
            this.DEDUCCIONES_EMPLEADOS = new HashSet<DEDUCCIONES_EMPLEADOS>();
        }
    
        public int idDeduccion { get; set; }
        public string nombreDeduccion { get; set; }
        public decimal porcentaje { get; set; }
        public string descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEDUCCIONES_EMPLEADOS> DEDUCCIONES_EMPLEADOS { get; set; }
    }
}