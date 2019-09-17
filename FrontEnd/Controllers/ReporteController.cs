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
    public class ReporteController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }
        //Generador de reportes por medio del Stored procedure.
        public ActionResult ReportEmpleadosConArma()
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

            return View("~/Views/Reporte/Report.cshtml");
        }
        //Generador de reportes por medio del Stored procedure.
        public ActionResult ReportPlanilla()
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
            string rutaReporte = "~/Reports/CalculoPlanillaReport.rdlc";
            ///construir la ruta física
            string rutaServidor = Server.MapPath(rutaReporte);
            reportViewer.LocalReport.ReportPath = rutaServidor;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportCategories.rdlc";
            var infoFuenteDatos = reportViewer.LocalReport.GetDataSourceNames();
            reportViewer.LocalReport.DataSources.Clear();

            List<sp_CalculoPlanilla_Result> datosReporte;
            using (BDContext context = new BDContext())
            {
                datosReporte = context.sp_CalculoPlanilla().ToList();
            }
            ReportDataSource fuenteDatos = new ReportDataSource();
            fuenteDatos.Name = infoFuenteDatos[0];
            fuenteDatos.Value = datosReporte;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PlanillaDataSet", datosReporte));

            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;


            return View("~/Views/Reporte/Report.cshtml");
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
    }
}