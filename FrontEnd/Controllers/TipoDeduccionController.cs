using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoDeduccionController : Controller
    {
        private TipoDeducionViewModel Convertir(TIPO_DEDUCCIONES tipoDeduccion)
        {
            TipoDeducionViewModel tipoDeduccionViewModel = new TipoDeducionViewModel
            {
                idDeduccion = tipoDeduccion.idDeduccion,
                nombreDeduccion = tipoDeduccion.nombreDeduccion,
                porcentaje = tipoDeduccion.porcentaje,
                descripcion = tipoDeduccion.descripcion,
            };
            return tipoDeduccionViewModel;
        }

        private TIPO_DEDUCCIONES Convertir(TipoDeducionViewModel tipoDeduccionViewModel)
        {
            TIPO_DEDUCCIONES deducciones = new TIPO_DEDUCCIONES
            {
                idDeduccion = tipoDeduccionViewModel.idDeduccion,
                nombreDeduccion = tipoDeduccionViewModel.nombreDeduccion,
                porcentaje = tipoDeduccionViewModel.porcentaje,
                descripcion = tipoDeduccionViewModel.descripcion,
            };
            return deducciones;
        }
        // GET: Deduccion
        public ActionResult Index()
        {
            List<TIPO_DEDUCCIONES> deducciones;
            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> Unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                deducciones = Unidad.genericDAL.GetAll().ToList();
            }

            List<TipoDeducionViewModel> lista = new List<TipoDeducionViewModel>();
            foreach (var item in deducciones)
            {
                lista.Add(this.Convertir(item));
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TipoDeducionViewModel tipoDeducionViewModel)
        {
            string mensaje = "";
            try
            {
                TIPO_DEDUCCIONES parametro = this.Convertir(tipoDeducionViewModel);
                mensaje = "Registro agregado satisfactoriamente";
                using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
                {
                    unidad.genericDAL.Add(parametro);
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
            TIPO_DEDUCCIONES parametro;
            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(parametro));
        }

        [HttpPost]
        public ActionResult Edit(TipoDeducionViewModel tipoDeducionViewModel)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro agregado satisfactoriamente";
                using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
                {
                    unidad.genericDAL.Update(this.Convertir(tipoDeducionViewModel));
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
            TIPO_DEDUCCIONES parametro;
            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(parametro));
        }

        public ActionResult Delete(int id)
        {
            TIPO_DEDUCCIONES parametro;
            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(parametro));
        }

        [HttpPost]
        public ActionResult Delete(TipoDeducionViewModel tipoDeducionViewModel)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro eliminado satisfactoriamente";
                using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
                {
                    unidad.genericDAL.Remove(this.Convertir(tipoDeducionViewModel));
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
    }
}