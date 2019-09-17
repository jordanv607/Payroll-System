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
    [Authorize(Roles = "Administrador,Planillero")]
    public class PersonaController : Controller
    {
        private PersonaViewModel Convertir(PERSONAS persona)
        {
            PersonaViewModel personaViewModel = new PersonaViewModel
            {
                idPersona = persona.idPersona,
                numeroIdentificacion = persona.numeroIdentificacion,
                nombrePersona = persona.nombrePersona,
                primerApellido = persona.primerApellido,
                segundoApellido = persona.segundoApellido,
                genero = persona.genero,
                estado = persona.estado,
                nacionalidad = persona.nacionalidad,
                correoElectronico = persona.correoElectronico,
                telefono = persona.telefono,
                direccionResidencia = persona.direccionResidencia
            };
            return personaViewModel;
        }

        private PERSONAS Convertir(PersonaViewModel personaViewModel)
        {
            PERSONAS persona = new PERSONAS
            {
                idPersona = personaViewModel.idPersona,
                numeroIdentificacion = personaViewModel.numeroIdentificacion,
                nombrePersona = personaViewModel.nombrePersona,
                primerApellido = personaViewModel.primerApellido,
                segundoApellido = personaViewModel.segundoApellido,
                genero = personaViewModel.genero,
                estado = personaViewModel.estado,
                nacionalidad = personaViewModel.nacionalidad,
                correoElectronico = personaViewModel.correoElectronico,
                telefono = personaViewModel.telefono,
                direccionResidencia = personaViewModel.direccionResidencia
            };
            return persona;
        }

        // GET: Empleado
        /// <summary>
        /// Método para desplegar información de los empleados (pag principal)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<PERSONAS> personas;
            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personas = Unidad.genericDAL.GetAll().ToList();
            }

            List<PersonaViewModel> lista = new List<PersonaViewModel>();
            PersonaViewModel personaVM = new PersonaViewModel();
            foreach (var item in personas)
            {
                personaVM = this.Convertir(item);

                using (UnidadDeTrabajo<PARAMETROS> Unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
                {
                    personaVM.generoObj = Unidad.genericDAL.Get(item.genero);
                    personaVM.estadoObj = Unidad.genericDAL.Get(item.estado);
                    personaVM.nacionalidadObj = Unidad.genericDAL.Get(item.nacionalidad);
                }
                lista.Add(personaVM);
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            PersonaViewModel personaViewModel = new PersonaViewModel { };

            personaViewModel.generos = this.parametros("personas", "genero");
            personaViewModel.estados = this.parametros("personas", "estado");
            personaViewModel.nacionalidades = this.parametros("personas", "nacionalidad");

            return View(personaViewModel);
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
        public ActionResult Create(PERSONAS persona)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro agregado satisfactoriamente";
                using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
                {
                    unidad.genericDAL.Add(persona);
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

        
        /*
        public ActionResult CrearEmpleado(int id)
        {
            PERSONAS personaEntity;
            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personaEntity = unidad.genericDAL.Get(id);
            }

            PersonaViewModel persona = this.Convertir(personaEntity);
            string ruta = "../Empleado/Crear/" + id;
            return RedirectToAction(ruta);

        }
        */
        public ActionResult Edit(int id)
        {
            PERSONAS personaEntity;
            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personaEntity = unidad.genericDAL.Get(id);
            }

            PersonaViewModel persona = this.Convertir(personaEntity);

            PARAMETROS parametroGenero;
            List<PARAMETROS> parametroGeneros;
            PARAMETROS parametroEstado;
            List<PARAMETROS> parametroEstados;
            PARAMETROS parametroNacionalidad;
            List<PARAMETROS> parametroNacionalidades;

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                parametroGenero = unidad.genericDAL.Get(persona.genero);
                parametroEstado = unidad.genericDAL.Get(persona.estado);
                parametroNacionalidad = unidad.genericDAL.Get(persona.nacionalidad);
            }
            parametroGeneros = this.parametros("personas", "genero");
            parametroGeneros.Insert(0, parametroGenero);
            persona.generos = parametroGeneros;

            parametroEstados = this.parametros("personas", "estado");
            parametroEstados.Insert(0, parametroEstado);
            persona.estados = parametroEstados;

            parametroNacionalidades = this.parametros("personas", "nacionalidad");
            parametroNacionalidades.Insert(0, parametroNacionalidad);
            persona.nacionalidades = parametroNacionalidades;

            return View(persona);
        }


        [HttpPost]
        public ActionResult Edit(PERSONAS persona)
        {
            string mensaje = "";
            try
            {
                
                using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
                {
                    unidad.genericDAL.Update(persona);
                    unidad.Complete();
                    mensaje = "Registro modificado satisfactoriamente";
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
            PERSONAS personaEntity;
            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                personaEntity = unidad.genericDAL.Get(id);
            }
            PersonaViewModel persona = this.Convertir(personaEntity);

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                persona.generoObj = unidad.genericDAL.Get(persona.genero);
                persona.estadoObj = unidad.genericDAL.Get(persona.estado);
                persona.nacionalidadObj = unidad.genericDAL.Get(persona.nacionalidad);
            }
            return View(persona);
        }

        public ActionResult Delete(int id)
        {
            PERSONAS peronaEntity;
            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                peronaEntity = unidad.genericDAL.Get(id);
            }

            PersonaViewModel persona = this.Convertir(peronaEntity);

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                persona.generoObj = unidad.genericDAL.Get(persona.genero);
                persona.estadoObj = unidad.genericDAL.Get(persona.estado);
                persona.nacionalidadObj = unidad.genericDAL.Get(persona.nacionalidad);
            }
            return View(persona);
        }

        [HttpPost]
        public ActionResult Delete(PERSONAS persona)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro eliminado satisfactoriamente";
                using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
                {
                    unidad.genericDAL.Remove(persona);
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