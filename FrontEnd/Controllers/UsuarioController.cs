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
    public class UsuarioController : Controller
    {
        private UsuarioViewModel Convertir(USUARIOS usuario)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel
            {
                idUsuario = usuario.idUsuario,
                idPersona =(int) usuario.idPersona,
                idRol =(int) usuario.idRol,
                usuario = usuario.usuario,
                contrasenia = usuario.contrasenia
            };
            return usuarioViewModel;
        }

        private USUARIOS Convertir(UsuarioViewModel usuarioViewModel)
        {
            USUARIOS usuario = new USUARIOS
            {
                idUsuario = usuarioViewModel.idUsuario,
                idPersona = (int)usuarioViewModel.idPersona,
                idRol = (int)usuarioViewModel.idRol,
                usuario = usuarioViewModel.usuario,
                contrasenia = usuarioViewModel.contrasenia
            };
            return usuario;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            string mensaje = "";

            if (Session["mensaje"] != null)
            {
                mensaje = TempData["mensaje"].ToString();
            }

            List<USUARIOS> usuarios;

            using (UnidadDeTrabajo<USUARIOS> Unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                usuarios = Unidad.genericDAL.GetAll().ToList();
            }
            List<UsuarioViewModel> usuariosVM = new List<UsuarioViewModel>();
            UsuarioViewModel usuarioViewModel;
            foreach (var item in usuarios)
            {
               
                usuarioViewModel = this.Convertir(item);
                usuariosVM.Add(usuarioViewModel);
                using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
                {
                    usuarioViewModel.persona = Unidad.genericDAL.Get(item.idPersona);
                }
                using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
                {
                    usuarioViewModel.Rol = Unidad.genericDAL.Get(item.idRol);
                }
            }
            return View(usuariosVM);
        }
        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            USUARIOS usuarioEntity;
            using (UnidadDeTrabajo<USUARIOS> Unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                usuarioEntity = Unidad.genericDAL.Get(id);
            }
            UsuarioViewModel usuario = this.Convertir(usuarioEntity);

            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                usuario.persona = Unidad.genericDAL.Get(usuario.idPersona);
            }

            using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                usuario.Rol = Unidad.genericDAL.Get(usuario.idRol);
            }
            return View(usuario);
        }
        public ActionResult Create()
        {
            UsuarioViewModel usuario = new UsuarioViewModel { };

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                usuario.Personas = unidad.genericDAL.GetAll().ToList();
            }
            using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                usuario.Roles = unidad.genericDAL.GetAll().ToList();
            }
           
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Create(UsuarioViewModel usuarioViewModel)
        {
            USUARIOS usu = this.Convertir(usuarioViewModel);
            using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                unidad.genericDAL.Add(usu);
                if (!unidad.Complete())
                {
                    TempData["mensaje"] = "El registro ya existe";
                    return RedirectToAction("Create");
                }
            }     
            TempData["mensaje"] = "Registro agregado satisfactoriamente";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            USUARIOS usuarioEntity;
            using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                usuarioEntity = unidad.genericDAL.Get(id);

            }

           UsuarioViewModel usuario = this.Convertir(usuarioEntity);

            PERSONAS persona;
            List<PERSONAS> personas;

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personas = unidad.genericDAL.GetAll().ToList();
                persona = unidad.genericDAL.Get(usuario.idPersona);
            }
            personas.Insert(0, persona);
            usuario.Personas = personas;


            ROLES role;
            List<ROLES> roles;

            using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                roles = unidad.genericDAL.GetAll().ToList();
                role = unidad.genericDAL.Get(usuario.idRol);
            }
            roles.Insert(0, role);
            usuario.Roles = roles;

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]

        public ActionResult Edit(UsuarioViewModel usuarioViewModel)
        {
            USUARIOS usu = this.Convertir(usuarioViewModel);
            {

                try
                {
                    using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
                    {
                        unidad.genericDAL.Update(usu);
                        unidad.Complete();
                    }
                    TempData["mensaje"] = "Registro modificado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["mensaje"] = "Error al modificar el registro";

                    return RedirectToAction("Edit");
                }

            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            USUARIOS usuarioEntity;
            using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                usuarioEntity = unidad.genericDAL.Get(id);

            }

            UsuarioViewModel usuario = this.Convertir(usuarioEntity);




            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {

                usuario.persona = unidad.genericDAL.Get(usuario.idPersona);
            }

            using (UnidadDeTrabajo<ROLES> unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {

                usuario.Rol = unidad.genericDAL.Get(usuario.idRol);
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(USUARIOS usuario)
        {

            using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                unidad.genericDAL.Remove(usuario);
                unidad.Complete();
            }
            TempData["mensaje"] = "Registro eliminado satisfactoriamente";
            return RedirectToAction("Index");
        }
    }
}
