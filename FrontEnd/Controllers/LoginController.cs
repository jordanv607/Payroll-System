using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private IUserDAL userDAL;

        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(UsuarioViewModel usuarioViewModel)
        {
            usuarioViewModel.usuario = Request["usuario"];
            usuarioViewModel.contrasenia = Request["contrasenia"];
            userDAL = new UserDALImpl();
            var userDetails = userDAL.getUser(usuarioViewModel.usuario, usuarioViewModel.contrasenia);

            if (userDetails == null)
            {
                Response.Write("Nombre de Usuario o Contraseña Incorrectos");
                //string script = "<script type='text/javascript'>alert('Nombre de Usuario o Password Incorrectos');</script>";
                //usuarioViewModel.LoginErrorMessage = "Nombre de Usuario o Password Incorrectos";
                return View("Index", usuarioViewModel);
            }
            else
            {

                Session["userID"] = userDetails.idUsuario;
                Session["userName"] = userDetails.usuario;
                var authTicket = new FormsAuthenticationTicket(userDetails.usuario, true, 100000);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                            FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                var name = User.Identity.Name; // line 4
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UsuarioViewModel usuarioViewModel)
        {
            usuarioViewModel.usuario = Request["usuario"];
            usuarioViewModel.contrasenia = Request["contrasenia"];
            PERSONAS p;
            List<PERSONAS> listP;
            string id;
            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                listP = Unidad.genericDAL.GetAll().ToList();
                id = this.RandomString(12);
                foreach (var item in listP)
                {
                    while(id == item.numeroIdentificacion)
                    {
                        id = this.RandomString(12); //genero una cedula unica
                    }
                }
                p = new PERSONAS
                {
                    correoElectronico = "generico",
                    estado = 1,
                    genero = 1,
                    nacionalidad = 1,
                    numeroIdentificacion = id,
                    nombrePersona = "generico",
                    segundoApellido = "generico",
                    telefono = "generico",
                    primerApellido = "generico",
                    
                };
                Unidad.genericDAL.Add(p);//creo una persona
                Unidad.Complete();
            }

            int ide  = 0;
            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {   
                listP = Unidad.genericDAL.GetAll().ToList();
                foreach (var item in listP)
                {
                    if(item.numeroIdentificacion == id)
                    {
                        ide = item.idPersona; //obtengo id de la persona
                    }
                }
                Unidad.Complete();
            }

            using (UnidadDeTrabajo<USUARIOS> Unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                usuarioViewModel.idPersona = ide;
                Unidad.genericDAL.Add(this.Convertir(usuarioViewModel));
                Unidad.Complete();
            }
                return RedirectToAction("Index", "Login");
        }

        private USUARIOS Convertir(UsuarioViewModel usuarioViewModel)
        {
            USUARIOS usuario = new USUARIOS
            {
                //idUsuario = usuarioViewModel.idUsuario,
                idPersona = usuarioViewModel.idPersona,
                idRol = 1,//(int)usuarioViewModel.idRol,
                usuario = usuarioViewModel.usuario,
                contrasenia = usuarioViewModel.contrasenia
            };
            return usuario;
        }

        private string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxy";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ActionResult LogOut()
        {
            int userId;
            if (Session["userID"] == null)
            {
                userId = 1;
            }
            else
            {
                userId = (int)Session["userID"];
            }
             
            Session.Clear();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return RedirectToAction("Index", "Login");
        }
    }
}