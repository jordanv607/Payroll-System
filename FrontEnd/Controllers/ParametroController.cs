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
    public class ParametroController : Controller
    {
        private ParametroViewModel Convertir(PARAMETROS parametro)
        {
            ParametroViewModel parametroViewModel = new ParametroViewModel
            {
                idParametro = parametro.idParametro,
                tabla = parametro.tabla,
                campo = parametro.campo,
                descripcion = parametro.descripcion,
            };
            return parametroViewModel;
        }

        private PARAMETROS Convertir(ParametroViewModel parametroViewModel)
        {
            PARAMETROS parametros = new PARAMETROS
            {
                idParametro = parametroViewModel.idParametro,
                tabla = parametroViewModel.tabla,
                campo = parametroViewModel.campo,
                descripcion = parametroViewModel.descripcion,
            };
            return parametros;
        }

        // GET: Parametro
        /// <summary>
        /// Método para desplegar información de los parámetros (pag principal)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<PARAMETROS> parametros;
            using (UnidadDeTrabajo<PARAMETROS> Unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                parametros = Unidad.genericDAL.GetAll().ToList();
            }

            List<ParametroViewModel> lista = new List<ParametroViewModel>();
            foreach (var item in parametros)
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
        public ActionResult Create(ParametroViewModel parametroViewModel)
        {
            string mensaje = "";
            try
            {
                PARAMETROS parametro = this.Convertir(parametroViewModel);
                mensaje = "Registro agregado satisfactoriamente";
                using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
                {
                    unidad.genericDAL.Add(parametro);

                    if (!unidad.Complete())
                    {
                        TempData["mensaje"] = "No puede agregar un registro sin datos";
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
            PARAMETROS parametro;
            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(parametro));
        }

        [HttpPost]
        public ActionResult Edit(ParametroViewModel parametroViewModel)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro modificado satisfactoriamente";
                using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
                {
                    unidad.genericDAL.Update(this.Convertir(parametroViewModel));
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
            PARAMETROS parametro;
            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(parametro));
        }

        public ActionResult Delete(int id)
        {
            PARAMETROS parametro;
            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(parametro));
        }

        [HttpPost]
        public ActionResult Delete(ParametroViewModel parametroViewModel)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro eliminado satisfactoriamente";
                using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
                {
                    unidad.genericDAL.Remove(this.Convertir(parametroViewModel));
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