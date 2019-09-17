using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    /*
     Esta clase contiene los metodos para el calculo de toda la planilla. Se trabaja sobre la tabla PAGO_EMPLEADOS
         */
    public class CalculoPlanilla
    {
        public bool calcularSalarioBruto()
        {
            try
            {
                //Obtengo una lista de todos los pagos
                List<PAGO_EMPLEADOS> pagos;
                using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
                {
                    pagos = Unidad.genericDAL.GetAll().ToList();

                    foreach (var item in pagos)
                    {
                        decimal deduccion = (item.salarioNeto) - (item.totalDeducciones);
                        item.salarioBruto = deduccion;
                        Unidad.genericDAL.Update(item);
                    }
                    Unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool calcularSalarioNeto()
        {
            try
            {
                List<EMPLEADOS> empleados;
                List<PAGO_EMPLEADOS> pagos;
                using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
                {
                    pagos = Unidad.genericDAL.GetAll().ToList();//obtengo todos los pagos


                    using (UnidadDeTrabajo<EMPLEADOS> Unidad1 = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
                    {
                        empleados = Unidad1.genericDAL.GetAll().ToList();//obtengo todos los empleados


                        foreach (var itemPagos in pagos)
                        {
                            foreach (var itemEmpleados in empleados)
                            {
                                if (itemPagos.idEmpleado == itemEmpleados.idEmpleado)
                                {
                                    itemPagos.salarioNeto = itemEmpleados.salarioMensual;
                                    Unidad.genericDAL.Update(itemPagos);
                                }
                            }
                        }
                        Unidad1.Complete();
                    }
                    Unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool calcularDeducciones()
        {
            try
            {
                List<DEDUCCIONES_EMPLEADOS> deducciones;
                List<PAGO_EMPLEADOS> pagos;
                using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
                {
                    pagos = Unidad.genericDAL.GetAll().ToList();//obtengo todos los pagos
                    using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> Unidad1 = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
                    {
                        deducciones = Unidad1.genericDAL.GetAll().ToList();//obtengo todas las deducciones

                        foreach (var itemPagos in pagos)
                        {
                            itemPagos.totalDeducciones = 0;
                            foreach (var itemDeducciones in deducciones)
                            {
                                if (itemPagos.idEmpleado == itemDeducciones.idEmpleado)
                                {
                                    itemPagos.totalDeducciones += itemDeducciones.monto;
                                    Unidad.genericDAL.Update(itemPagos);
                                }
                            }
                        }

                        Unidad1.Complete();
                    }
                    Unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool actualizarLista(DateTime date)
        {
            try
            {
                List<EMPLEADOS> empleados;
                List<PAGO_EMPLEADOS> pagos;
                using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
                {
                    pagos = Unidad.genericDAL.GetAll().ToList();//obtengo todos los pagos
                    
                    if(pagos.Count != 0)
                    {
                        foreach (var item in pagos)
                        {
                            Unidad.genericDAL.Remove(item);//elimino todos los registros
                            //Unidad.Complete();
                        }
                    }
                    using (UnidadDeTrabajo<EMPLEADOS> Unidad1 = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
                    {
                        empleados = Unidad1.genericDAL.GetAll().ToList();//obtengo todas los empleados
                        if(empleados.Count != 0)
                        {
                            foreach (var item in empleados)
                            {
                                PAGO_EMPLEADOS x = new PAGO_EMPLEADOS
                                {
                                    idEmpleado = item.idEmpleado,
                                    salarioBruto = 0,
                                    totalDeducciones = 0,
                                    salarioNeto = 0,
                                    fechaPago = date
                                };
                                Unidad.genericDAL.Add(x);//agrego de nuevo a todos los empleados disponibles
                                                         //Unidad.Complete();
                            }
                        }
                        Unidad1.Complete();
                    }
                    Unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}



