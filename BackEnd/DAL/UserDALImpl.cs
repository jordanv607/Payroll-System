using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Entities;

namespace BackEnd.DAL
{
    public class UserDALImpl : IUserDAL
    {
        private BDContext context;
        public string[] gerRolesForUser(string usuario)
        {
            string[] result;
            using (context = new BDContext())
            {
                result = context.sp_getRolesForUser(usuario).ToArray();
            }
            return result;
        }

        public USUARIOS getUser(string usuario)
        {
            try
            {
                USUARIOS resultado;
                using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
                {
                    Expression<Func<USUARIOS, bool>> consulta = (u => u.usuario.Equals(usuario));
                    resultado = unidad.genericDAL.Find(consulta).ToList().FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public USUARIOS getUser(string usuario, string contrasenia)
        {
            try
            {
                USUARIOS resultado;
                using (UnidadDeTrabajo<USUARIOS> unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
                {
                    Expression<Func<USUARIOS, bool>> consulta = (u => u.usuario.Equals(usuario) && u.contrasenia.Equals(contrasenia));
                    resultado = unidad.genericDAL.Find(consulta).ToList().FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<USUARIOS> getUsuariosRole(string nombreRol)
        {
            List<USUARIOS> result = new List<USUARIOS>();
            List<string> lista;
            using (context = new BDContext())
            {
                lista = context.sp_getUsuariosRole(nombreRol).ToList();
                USUARIOS user;
                foreach (var item in lista)
                {
                    user = this.getUser(item);
                    result.Add(user);
                }
            }
            return result;
        }

        public bool isUserInRole(string usuario, string nombreRol)
        {
            bool result = false;
            using (context = new BDContext())
            {
                result = (bool)context.sp_isUserInRole(usuario, nombreRol).First();
                //  result  = (bool)context.sp_isUserInRole(userName, roleName).FirstOrDefault();
            }
            return result;
        }
    }
}
