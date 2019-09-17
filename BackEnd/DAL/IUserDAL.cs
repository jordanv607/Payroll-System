using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IUserDAL
    {
        USUARIOS getUser(string usuario);
        USUARIOS getUser(string usuario, string contrasenia);
        bool isUserInRole(string usuario, string nombreRol);
        string[] gerRolesForUser(string usuario);
        List<USUARIOS> getUsuariosRole(string nombreRol);

    }
}