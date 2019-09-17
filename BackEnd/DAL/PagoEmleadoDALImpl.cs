using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Entities;

namespace BackEnd.DAL
{
    public class PagoEmleadoDALImpl : IPagoEmpleado
    {
        private BDContext context;

        public decimal calcularDeducciones(PAGO_EMPLEADOS pago)
        {
            decimal totalDeducciones = 0;
            List<TIPO_DEDUCCIONES> tipoDeducciones = this.getDeducciones();
            DEDUCCIONES_EMPLEADOS deduccioneEmpleado = new DEDUCCIONES_EMPLEADOS();
            deduccioneEmpleado.fechaPago = pago.fechaPago;
            deduccioneEmpleado.idEmpleado = pago.idEmpleado;
            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {
                foreach (var itemDeucciones in tipoDeducciones)
                {
                    deduccioneEmpleado.idDeduccion = itemDeucciones.idDeduccion;
                    deduccioneEmpleado.monto = pago.salarioBruto * (itemDeucciones.porcentaje / 100);
                    totalDeducciones = totalDeducciones + pago.salarioBruto * (itemDeucciones.porcentaje / 100);
                    unidad.genericDAL.Add(deduccioneEmpleado);
                    unidad.Complete();
                }
            }
            return totalDeducciones;
        }

        public bool calcularSalarioNeto(DateTime fechaPago)
        {
            try
            {
                List<sp_getEmpleadosActivos_Result> empleadosActivos = this.getEmpleadosActivos();
                PAGO_EMPLEADOS pago = new PAGO_EMPLEADOS();
                pago.fechaPago = fechaPago;

                using (UnidadDeTrabajo<PAGO_EMPLEADOS> unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
                {
                    foreach (var itemEmpleados in empleadosActivos)
                    {
                        pago.idEmpleado = itemEmpleados.idEmpleado;
                        pago.salarioBruto = itemEmpleados.salarioMensual;//si los pagos van a ser quincenales hay que dividir entre 2
                        pago.totalDeducciones = this.calcularDeducciones(pago);
                        pago.salarioNeto = pago.salarioBruto - pago.totalDeducciones;
                        unidad.genericDAL.Add(pago);
                        unidad.Complete();
                    }
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TIPO_DEDUCCIONES> getDeducciones()
        {
            List<TIPO_DEDUCCIONES> tipoDeducciones;
            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> Unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                tipoDeducciones = Unidad.genericDAL.GetAll().ToList();
            }
            return tipoDeducciones;
        }

        public List<sp_getEmpleadosActivos_Result> getEmpleadosActivos()
        {
            try
            {
                List<sp_getEmpleadosActivos_Result> empleadosActivos;
                using (context = new BDContext())
                {
                    empleadosActivos = context.sp_getEmpleadosActivos().ToList();
                }
                return empleadosActivos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
