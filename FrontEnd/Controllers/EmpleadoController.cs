using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador,Planillero")]
    public class EmpleadoController : Controller
    {
        private EmpleadoViewModel Convertir(EMPLEADOS empleado)
        {
            EmpleadoViewModel empleadoViewModel = new EmpleadoViewModel
            {
                idEmpleado = empleado.idEmpleado,
                idPersona = empleado.idPersona,
                departamento = empleado.departamento,
                puesto = empleado.puesto,
                salarioMensual = empleado.salarioMensual,
                portacionArma = empleado.portacionArma,
                vencimientoPermisoArma = empleado.vencimientoPermisoArma,
                numeroCuentaBancaria = empleado.numeroCuentaBancaria
            };
            return empleadoViewModel;
        }

        private EMPLEADOS Convertir(EmpleadoViewModel empleadoViewModel)
        {
            EMPLEADOS empleado = new EMPLEADOS
            {
                idEmpleado = empleadoViewModel.idEmpleado,
                idPersona = empleadoViewModel.idPersona,
                departamento = empleadoViewModel.departamento,
                puesto = empleadoViewModel.puesto,
                salarioMensual = empleadoViewModel.salarioMensual,
                portacionArma = empleadoViewModel.portacionArma,
                vencimientoPermisoArma = empleadoViewModel.vencimientoPermisoArma,
                numeroCuentaBancaria = empleadoViewModel.numeroCuentaBancaria
            };
            return empleado;
        }

        // GET: Empleado
        /// <summary>
        /// Método para desplegar información de los empleados (pag principal)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<EMPLEADOS> empleados;
            using (UnidadDeTrabajo<EMPLEADOS> Unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                empleados = Unidad.genericDAL.GetAll().ToList();
            }

            List<EmpleadoViewModel> lista = new List<EmpleadoViewModel>();
            EmpleadoViewModel empleadoVM = new EmpleadoViewModel();
            foreach (var item in empleados)
            {
                empleadoVM = this.Convertir(item);

                using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
                {
                    empleadoVM.empleado = Unidad.genericDAL.Get(item.idPersona);
                }
                using (UnidadDeTrabajo<PARAMETROS> Unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
                {
                    empleadoVM.depto = Unidad.genericDAL.Get(item.departamento);
                    empleadoVM.ocupacion = Unidad.genericDAL.Get(item.puesto);
                    empleadoVM.portaArma = Unidad.genericDAL.Get(item.portacionArma);
                }

                lista.Add(empleadoVM);
            }
            return View(lista);
        }
        /*
        /// <summary>
/// para crear empleado seleccionado el número de cédula
/// </summary>
/// <returns></returns>
        public ActionResult Create()
        {
            EmpleadoViewModel empleadoViewModel = new EmpleadoViewModel { };

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                empleadoViewModel.empleados = unidad.genericDAL.GetAll().ToList();
            }
            empleadoViewModel.departamentos = this.parametros("empleados", "departamento");
            empleadoViewModel.puestos = this.parametros("empleados", "puesto");
            empleadoViewModel.portacionArmas = this.parametros("empleados", "portacionArma");

            return View(empleadoViewModel);
        }
        */
        public ActionResult Create(Nullable<int> id)
        {
            EmpleadoViewModel empleadoVM = new EmpleadoViewModel();
            
            PERSONAS persona;
            List<PERSONAS> personas;
            
            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personas = unidad.genericDAL.GetAll().ToList();
                persona = unidad.genericDAL.Get(Convert.ToInt16(id));
            }
            personas.Insert(0, persona);
            empleadoVM.idPersona = Convert.ToInt16(id);
            empleadoVM.empleados = personas;

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                empleadoVM.empleados = unidad.genericDAL.GetAll().ToList();
            }
            empleadoVM.departamentos = this.parametros("empleados", "departamento");
            empleadoVM.puestos = this.parametros("empleados", "puesto");
            empleadoVM.portacionArmas = this.parametros("empleados", "portacionArma");

            return View(empleadoVM);
        }

        List<PARAMETROS> parametros(string filtroTabla, string filtroCampo)
        {
            BDContext context;
            using (context = new BDContext())
            {
                return (from c in context.PARAMETROS
                        where c.tabla == filtroTabla
                        where c.campo == filtroCampo
                        select c).ToList();
            }
        }

        [HttpPost]
        public ActionResult Create(EMPLEADOS empleado)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro agregado satisfactoriamente";
                using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
                {
                    unidad.genericDAL.Add(empleado);
                    if (!unidad.Complete())
                    {
                        TempData["mensaje"] = "El registro ya existe";
                        return RedirectToAction("Create");
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            TempData["mensaje"] = mensaje;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            EMPLEADOS empleadoEntity;
            using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                empleadoEntity = unidad.genericDAL.Get(id);
            }

            EmpleadoViewModel empleado = this.Convertir(empleadoEntity);

            PERSONAS persona;
            List<PERSONAS> personas;

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personas = unidad.genericDAL.GetAll().ToList();
                persona = unidad.genericDAL.Get(empleado.idPersona);
            }
            personas.Insert(0, persona);
            empleado.empleados = personas;


            PARAMETROS parametroDepartamento;
            List<PARAMETROS> parametroDepartamentos;
            PARAMETROS parametroPuesto;
            List<PARAMETROS> parametroPuestos;
            PARAMETROS parametroPortacionArma;
            List<PARAMETROS> parametroPortacionArmas;

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                parametroDepartamento = unidad.genericDAL.Get(empleado.departamento);
                parametroPuesto = unidad.genericDAL.Get(empleado.puesto);
                parametroPortacionArma = unidad.genericDAL.Get(empleado.portacionArma);
            }
            parametroDepartamentos = this.parametros("empleados", "departamento");
            parametroDepartamentos.Insert(0, parametroDepartamento);
            empleado.departamentos = parametroDepartamentos;

            parametroPuestos = this.parametros("empleados", "puesto");
            parametroPuestos.Insert(0, parametroPuesto);
            empleado.puestos = parametroPuestos;

            parametroPortacionArmas = this.parametros("empleados", "portacionArma");
            parametroPortacionArmas.Insert(0, parametroPortacionArma);
            empleado.portacionArmas = parametroPortacionArmas;

            return View(empleado);
        }


        [HttpPost]
        public ActionResult Edit(EMPLEADOS empleado)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro modificado satisfactoriamente";
                using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
                {
                    unidad.genericDAL.Update(empleado);
                    unidad.Complete();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();

            }
            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            EMPLEADOS empleadoEntity;
            using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                empleadoEntity = unidad.genericDAL.Get(id);
            }
            EmpleadoViewModel empleado = this.Convertir(empleadoEntity);

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                empleado.empleado = unidad.genericDAL.Get(empleado.idPersona);
            };

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                empleado.depto = unidad.genericDAL.Get(empleado.departamento);
                empleado.ocupacion = unidad.genericDAL.Get(empleado.puesto);
                empleado.portaArma = unidad.genericDAL.Get(empleado.portacionArma);
            }
            return View(empleado);
        }

        public ActionResult Delete(int id)
        {
            EMPLEADOS empleadoEntity;
            using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                empleadoEntity = unidad.genericDAL.Get(id);
            }

            EmpleadoViewModel empleado = this.Convertir(empleadoEntity);
            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                empleado.empleado = unidad.genericDAL.Get(empleado.idPersona);
            };

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                empleado.depto = unidad.genericDAL.Get(empleado.departamento);
                empleado.ocupacion = unidad.genericDAL.Get(empleado.puesto);
                empleado.portaArma = unidad.genericDAL.Get(empleado.portacionArma);
            }
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Delete(EMPLEADOS empleado)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro eliminado satisfactoriamente";
                using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                unidad.genericDAL.Remove(empleado);
                unidad.Complete();
            }
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();

            }
            TempData["mensaje"] = mensaje;

            return RedirectToAction("Index");
        }
        //Generador de reportes por medio del Stored procedure.
        public ActionResult Report()
        {
            var reportViewer = new ReportViewer
            {

                ProcessingMode = ProcessingMode.Local,
                ShowExportControls = true,
                ShowParameterPrompts = true,
                ShowPageNavigationControls = true,
                ShowRefreshButton = true,
                ShowPrintButton = true,
                SizeToReportContent = true,
                AsyncRendering = false,
            };
            string rutaReporte = "~/Reports/PortacionArmaReport.rdlc";
            ///construir la ruta física
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<sp_RetornaPortacionArma_Result> datosReporte;
            using (BDContext context = new BDContext())
            {
                datosReporte = context.sp_RetornaPortacionArma().ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("sp_RetornaArmaDataSource", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;


            return View();
        }
    }
}