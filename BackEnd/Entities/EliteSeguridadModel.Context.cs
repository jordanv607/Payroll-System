﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BDContext : DbContext
    {
        public BDContext()
            : base("name=BDContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DEDUCCIONES_EMPLEADOS> DEDUCCIONES_EMPLEADOS { get; set; }
        public virtual DbSet<EMPLEADOS> EMPLEADOS { get; set; }
        public virtual DbSet<PAGO_EMPLEADOS> PAGO_EMPLEADOS { get; set; }
        public virtual DbSet<PARAMETROS> PARAMETROS { get; set; }
        public virtual DbSet<PERSONAS> PERSONAS { get; set; }
        public virtual DbSet<ROLES> ROLES { get; set; }
        public virtual DbSet<TIPO_DEDUCCIONES> TIPO_DEDUCCIONES { get; set; }
        public virtual DbSet<USUARIOS> USUARIOS { get; set; }
    
        public virtual ObjectResult<string> sp_getRolesForUser(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_getRolesForUser", userNameParameter);
        }
    
        public virtual ObjectResult<string> sp_getUsuariosRole(string roleName)
        {
            var roleNameParameter = roleName != null ?
                new ObjectParameter("roleName", roleName) :
                new ObjectParameter("roleName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_getUsuariosRole", roleNameParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> sp_isUserInRole(string userName, string roleName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var roleNameParameter = roleName != null ?
                new ObjectParameter("roleName", roleName) :
                new ObjectParameter("roleName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("sp_isUserInRole", userNameParameter, roleNameParameter);
        }
    
        public virtual ObjectResult<sp_CalculoPlanilla_Result> sp_CalculoPlanilla()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_CalculoPlanilla_Result>("sp_CalculoPlanilla");
        }
    
        public virtual ObjectResult<sp_RetornaPortacionArma_Result> sp_RetornaPortacionArma()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RetornaPortacionArma_Result>("sp_RetornaPortacionArma");
        }
    
        public virtual ObjectResult<sp_getEmpleadosActivos_Result> sp_getEmpleadosActivos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getEmpleadosActivos_Result>("sp_getEmpleadosActivos");
        }
    
        public virtual ObjectResult<sp_getDatosPlanillaCedula_Result> sp_getDatosPlanillaCedula(string pCedula)
        {
            var pCedulaParameter = pCedula != null ?
                new ObjectParameter("pCedula", pCedula) :
                new ObjectParameter("pCedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getDatosPlanillaCedula_Result>("sp_getDatosPlanillaCedula", pCedulaParameter);
        }
    
        public virtual ObjectResult<sp_getDatosPlanillaFecha_Result> sp_getDatosPlanillaFecha(Nullable<System.DateTime> pFecha)
        {
            var pFechaParameter = pFecha.HasValue ?
                new ObjectParameter("pFecha", pFecha) :
                new ObjectParameter("pFecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getDatosPlanillaFecha_Result>("sp_getDatosPlanillaFecha", pFechaParameter);
        }
    }
}