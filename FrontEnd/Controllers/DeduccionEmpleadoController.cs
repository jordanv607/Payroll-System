using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador,Planillero")]
    public class DeduccionEmpleadoController : Controller
    {
        private DeduccionEmpleadoViewModel Convertir(DEDUCCIONES_EMPLEADOS deduccionEmpleado)
        {
            DeduccionEmpleadoViewModel deduccionEmpleadoViewModel = new DeduccionEmpleadoViewModel
            {
                idDeduccionEmpleado = deduccionEmpleado.idDeduccionEmpleado,
                idEmpleado = deduccionEmpleado.idEmpleado,
                idDeduccion = Convert.ToString(deduccionEmpleado.idDeduccion),
                monto = deduccionEmpleado.monto,
                fechaPago = deduccionEmpleado.fechaPago
            };
            return deduccionEmpleadoViewModel;
        }

        private DEDUCCIONES_EMPLEADOS Convertir(DeduccionEmpleadoViewModel deduccionEmpleadoViewModel)
        {
            DEDUCCIONES_EMPLEADOS deducciones = new DEDUCCIONES_EMPLEADOS
            {
                idDeduccionEmpleado = deduccionEmpleadoViewModel.idDeduccionEmpleado,
                idEmpleado = deduccionEmpleadoViewModel.idEmpleado,
                idDeduccion = Convert.ToInt32(deduccionEmpleadoViewModel.idDeduccion),
                monto = deduccionEmpleadoViewModel.monto,
                fechaPago = deduccionEmpleadoViewModel.fechaPago
            };
            return deducciones;
        }
        // GET: Deduccion
        public ActionResult Index()
        {


            List<DEDUCCIONES_EMPLEADOS> deducciones;
            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> Unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {
                deducciones = Unidad.genericDAL.GetAll().ToList();
            }

            List<DeduccionEmpleadoViewModel> lista = new List<DeduccionEmpleadoViewModel>();
            DeduccionEmpleadoViewModel Dvm = new DeduccionEmpleadoViewModel();
            foreach (var item in deducciones)
            {

                Dvm = this.Convertir(item);



                using (var context = new BDContext())
                {

                    var result = context
                        .Database
                        .SqlQuery<string>("SELECT p.numeroIdentificacion as idEmpleado FROM PERSONAS p, EMPLEADOS e WHERE e.idPersona = p.idPersona and e.idEmpleado = @pIdEMpleado ",
                        new SqlParameter("@pIdEMpleado", item.idEmpleado)).FirstOrDefault();

                    Dvm.idEmpleado = Convert.ToInt32(result);
                }

                using (var context = new BDContext())
                {

                    var result = context
                        .Database
                        .SqlQuery<string>("SELECT t.nombreDeduccion FROM TIPO_DEDUCCIONES t, DEDUCCIONES_EMPLEADOS d WHERE  t.idDeduccion = d.idDeduccion and d.idDeduccion =  @pIdDeduccion ",
                        new SqlParameter("@pIdDeduccion", item.idDeduccion)).FirstOrDefault();

                    Dvm.idDeduccion = result;
                }


                lista.Add(Dvm);


            }

            return View(lista);
        }

        public ActionResult Create()
        {
            DeduccionEmpleadoViewModel deduccionesEmpleado = new DeduccionEmpleadoViewModel { };


            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                deduccionesEmpleado.TipoDeduccion = unidad.genericDAL.GetAll().ToList();
            }

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                deduccionesEmpleado.Personas = unidad.genericDAL.GetAll().ToList();
            }


            return View(deduccionesEmpleado);
        }

        [HttpPost]
        public ActionResult Create(DEDUCCIONES_EMPLEADOS deduccionEmpleado)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro agregado satisfactoriamente";

                using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
                {

                    using (var context = new BDContext())
                    {


                        var result = context
                              .Database
                              .SqlQuery<int>("SELECT e.idEmpleado as idEmpleado FROM PERSONAS p, EMPLEADOS e WHERE p.idPersona = e.idPersona and p.idPersona = @pIdEMpleado ",
                              new SqlParameter("@pIdEMpleado", deduccionEmpleado.idEmpleado)).FirstOrDefault();

                        deduccionEmpleado.idEmpleado = Convert.ToInt32(result);


                    }

                    unidad.genericDAL.Add(deduccionEmpleado);


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
            DEDUCCIONES_EMPLEADOS DeducionEmpleadoEntity;
            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {
                DeducionEmpleadoEntity = unidad.genericDAL.Get(id);
            }

            DeduccionEmpleadoViewModel deducionEmpleado = this.Convertir(DeducionEmpleadoEntity);

            PERSONAS persona;
            List<PERSONAS> personas;

            using (UnidadDeTrabajo<PERSONAS> unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                //Se llena combobox
                personas = unidad.genericDAL.GetAll().ToList();
                //Se optine la pocion inicial


                using (var context = new BDContext())
                {


                    var result = context
                        .Database
                        .SqlQuery<int>("SELECT p.idPersona FROM PERSONAS p, EMPLEADOS e WHERE  p.idPersona=e.idPersona and idEmpleado = @pIdEMpleado ",
                        new SqlParameter("@pIdEMpleado", deducionEmpleado.idEmpleado)).FirstOrDefault();


                    persona = unidad.genericDAL.Get(deducionEmpleado.idEmpleado);

                    deducionEmpleado.idEmpleado = result;
                    personas.Insert(0, persona);
                    deducionEmpleado.Personas = personas;
                }

            }
            // se le da la poccion inicial





            TIPO_DEDUCCIONES tipoDeduccion;
            List<TIPO_DEDUCCIONES> tipodDeducciones;

            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                tipodDeducciones = unidad.genericDAL.GetAll().ToList();
                tipoDeduccion = unidad.genericDAL.Get(deducionEmpleado.idDeduccionEmpleado);
            }
            tipodDeducciones.Insert(0, tipoDeduccion);
            deducionEmpleado.TipoDeduccion = tipodDeducciones;




            return View(deducionEmpleado);
        }


        [HttpPost]
        public ActionResult Edit(DEDUCCIONES_EMPLEADOS duducionEmpleado)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro modificado satisfactoriamente";
                using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
                {

                    using (var context = new BDContext())
                    {


                        var result = context
                              .Database
                              .SqlQuery<int>("SELECT e.idEmpleado as idEmpleado FROM PERSONAS p, EMPLEADOS e WHERE p.idPersona = e.idPersona and p.idPersona = @pIdEMpleado ",
                              new SqlParameter("@pIdEMpleado", duducionEmpleado.idEmpleado)).FirstOrDefault();

                        duducionEmpleado.idEmpleado = result;


                    }
                    unidad.genericDAL.Update(duducionEmpleado);
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
            DEDUCCIONES_EMPLEADOS deduccionEmpleado;
            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {
                deduccionEmpleado = unidad.genericDAL.Get(id);
            }


            DeduccionEmpleadoViewModel deduccionesEmpleado = this.Convertir(deduccionEmpleado);

            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {

                using (var context = new BDContext())
                {


                    var result = context
                        .Database
                        .SqlQuery<string>("SELECT p.numeroIdentificacion as idEmpleado FROM PERSONAS p, EMPLEADOS e WHERE e.idPersona = p.idPersona and e.idEmpleado = @pIdEMpleado ",
                        new SqlParameter("@pIdEMpleado", deduccionesEmpleado.idEmpleado)).FirstOrDefault();

                    deduccionesEmpleado.idEmpleado = Convert.ToInt32(result);
                }

            }
            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {

                using (var context = new BDContext())
                {

                    var result = context
                        .Database
                        .SqlQuery<string>("SELECT t.nombreDeduccion FROM TIPO_DEDUCCIONES t, DEDUCCIONES_EMPLEADOS d WHERE  t.idDeduccion = d.idDeduccion and d.idDeduccion =  @pIdDeduccion ",
                        new SqlParameter("@pIdDeduccion", deduccionEmpleado.idDeduccion)).FirstOrDefault();

                    deduccionesEmpleado.idDeduccion = result;

                }
                return View(deduccionesEmpleado);
            }


        }

        public ActionResult Delete(int id)
        {
            DEDUCCIONES_EMPLEADOS parametro;
            using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
            {
                parametro = unidad.genericDAL.Get(id);
            }

            DeduccionEmpleadoViewModel deduccionesEmpleado = new DeduccionEmpleadoViewModel { };

            using (UnidadDeTrabajo<EMPLEADOS> unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                deduccionesEmpleado.EMPLEADOS = unidad.genericDAL.GetAll().ToList();
            }

            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                deduccionesEmpleado.TipoDeduccion = unidad.genericDAL.GetAll().ToList();
            }
            return View(this.Convertir(parametro));
        }

        [HttpPost]
        public ActionResult Delete(DeduccionEmpleadoViewModel tipoDeducionViewModel)
        {
            string mensaje = "";
            try
            {
                mensaje = "Registro eliminado satisfactoriamente";
                using (UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS> unidad = new UnidadDeTrabajo<DEDUCCIONES_EMPLEADOS>(new BDContext()))
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