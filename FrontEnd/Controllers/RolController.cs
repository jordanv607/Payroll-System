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
    public class RolController : Controller
    {
        private RolViewModel Convertir(ROLES roles)
        {
            return new RolViewModel
            {
                idRol = roles.idRol,
                nombreRol = roles.nombreRol,
                descripcionRol = roles.descripcionRol
                
            };
        }
        // GET: Rol
        public ActionResult Index()
        {
        List<ROLES> roles;
            using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                roles = Unidad.genericDAL.GetAll().ToList();
            }
            List<RolViewModel> rolesVM = new List<RolViewModel>();
            RolViewModel rolesViewModel;
            foreach (var item in roles)
            {
                rolesViewModel = this.Convertir(item);
                rolesVM.Add(rolesViewModel);
            }
            return View(rolesVM);
        }

        // GET: roles/Details/5
        public ActionResult Details(int id)
        {
            ROLES rolesEntity;
            using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                rolesEntity = Unidad.genericDAL.Get(id);
            }
            RolViewModel roles = this.Convertir(rolesEntity);

            return View(roles);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ROLES roles)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro agregado satisfactoriamente";
                using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
                {
                    unidad.genericDAL.Add(roles);
                    if (!unidad.Complete())
                    {
                        TempData["mensaje"] = "El registro ya existe";
                        return RedirectToAction("Create");
                    }
                }
            } catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            TempData["mensaje"] = mensaje;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ROLES roles;
            using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                roles = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(roles));
        }
        [HttpPost]
        public ActionResult Edit(ROLES role)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro modificado satisfactoriamente";

                using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
                {
                    unidad.genericDAL.Update(role);
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

        public ActionResult Delete(int id)
        {
            ROLES roles;
            using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                roles = unidad.genericDAL.Get(id);
            }
            return View(this.Convertir(roles));
        }

        [HttpPost]
        public ActionResult Delete(ROLES role)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro eliminado satisfactoriamente";
                using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
                {
                    unidad.genericDAL.Remove(role);
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