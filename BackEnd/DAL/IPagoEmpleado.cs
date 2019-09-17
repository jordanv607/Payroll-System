using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IPagoEmpleado
    {
        List<sp_getEmpleadosActivos_Result> getEmpleadosActivos();
        decimal calcularDeducciones(PAGO_EMPLEADOS pago);
        bool calcularSalarioNeto(DateTime fechaPago);
        List<TIPO_DEDUCCIONES> getDeducciones();
        
    }
}
