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
    public class PagoEmpleadosController : Controller
    {
        CalculoPlanilla c = new CalculoPlanilla();
        IPagoEmpleado pagoDAL = new PagoEmleadoDALImpl();
        // GET: PagoEmpleados
        public ActionResult Index()
        {
            List<PlanillaViewModel> lista = new List<PlanillaViewModel>();
            List<sp_CalculoPlanilla_Result> listaSP;
            PlanillaViewModel planillaVM = new PlanillaViewModel();

            using (BDContext context = new BDContext())
            {
                listaSP = context.sp_CalculoPlanilla().ToList();
            }

            foreach (var item in listaSP)
            {
                planillaVM = this.Convertir(item);
                lista.Add(planillaVM);
            }

            return View(lista);
        }

        //Generador de reportes por medio del Stored procedure.
        public ActionResult ReportePlanillaFecha(ReporteViewModel reporteVM)
        {
            try
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
                string rutaReporte = "~/Reports/DatosPlanilla.rdlc";
                ///construir la ruta física
                string rutaServidor = Server.MapPath(rutaReporte);
                reportViewer.LocalReport.ReportPath = rutaServidor;
                //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
                var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
                reportViewer.LocalReport.DataSources.Clear();

                List<sp_getDatosPlanillaFecha_Result> datosReporte;
                using (BDContext context = new BDContext())
                {
                    datosReporte = context.sp_getDatosPlanillaFecha(reporteVM.fechaPago).ToList();
                }
                ReportDataSource fuenteDatos = new ReportDataSource();
                fuenteDatos.Name = infoFuenteDatos[0];
                fuenteDatos.Value = datosReporte;
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PlanillaDataSet", datosReporte));

                reportViewer.LocalReport.Refresh();
                ViewBag.ReportViewer = reportViewer;


                return View("~/Views/Reporte/Report.cshtml");
            }
            catch (Exception)
            {

                return View();
            }
        }

        private PlanillaViewModel Convertir(sp_CalculoPlanilla_Result planillero)
        {
            PlanillaViewModel planillaViewModel = new PlanillaViewModel
            {
                //idPersona = persona.idPersona,
                numeroIdentificacion = planillero.numeroIdentificacion,
                nombrePersona = planillero.nombrePersona,
                correoElectronico = planillero.correoElectronico,
                fechaPago = planillero.fechaPago,
                numeroCuentaBancaria = planillero.numeroCuentaBancaria,
                primerApellido = planillero.primerApellido,
                salarioBruto = planillero.salarioBruto,
                salarioNeto = planillero.salarioNeto,
                segundoApellido = planillero.segundoApellido,
                totalDeducciones = planillero.totalDeducciones
            };
            return planillaViewModel;
        }


        public ActionResult CalcularPlanilla()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalcularPlanilla(PAGO_EMPLEADOS pe)
        {
            /*//calculo metodos
            c.actualizarLista(pe.fechaPago);
            c.calcularSalarioNeto();
            c.calcularDeducciones();
            c.calcularSalarioBruto();*/
            pagoDAL.calcularSalarioNeto(pe.fechaPago);

            return RedirectToAction("Index");
        }

        public ActionResult ReportePlanillaCedula()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReportePlanillaCedula(ReporteViewModel reporteVM)
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
            string rutaReporte = "~/Reports/DatosPlanilla.rdlc";
            ///construir la ruta física
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<sp_getDatosPlanillaCedula_Result> datosReporte;
            using (BDContext context = new BDContext())
            {
                datosReporte = context.sp_getDatosPlanillaCedula(reporteVM.idEmpleado).ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PlanillaDataSet", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;


            return View("~/Views/Reporte/Report.cshtml");

        }
    }
}

