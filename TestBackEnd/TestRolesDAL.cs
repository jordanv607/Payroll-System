using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using BackEnd.Entities;
using BackEnd.DAL;

namespace TestBackEnd
{
    /// <summary>
    /// Descripción resumida de TestRolesDAL
    /// </summary>
    [TestClass]
    public class TestRolesDAL
    {
        public TestRolesDAL()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestAddGeneric()
        {


            ROLES roles = new ROLES
            {
                nombreRol = "Prueba Generics",
                descripcionRol = "esto es una prueba"
            };


            using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                Unidad.genericDAL.Add(roles);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestDeleteGeneric()
        {
            ROLES roles = new ROLES();

            using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                roles = Unidad.genericDAL.Get(2);
                Unidad.genericDAL.Remove(roles);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateGeneric()
        {
            ROLES roles;


            using (UnidadDeTrabajo<ROLES> Unidad = new UnidadDeTrabajo<ROLES>(new BDContext()))
            {
                roles = Unidad.genericDAL.Get(3);
                roles.descripcionRol = "modificado";
                Unidad.genericDAL.Update(roles);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }
    }
}
