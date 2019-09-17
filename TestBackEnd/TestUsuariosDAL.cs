using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.Entities;
using BackEnd.DAL;

namespace TestBackEnd
{
    /// <summary>
    /// Descripción resumida de TestUsuariosDAL
    /// </summary>
    [TestClass]
    public class TestUsuariosDAL
    {
        public TestUsuariosDAL()
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

            USUARIOS x = new USUARIOS
            {
                idPersona = 1,
                idRol = 1,
                usuario = "jordan",
                contrasenia ="123"
            };


            using (UnidadDeTrabajo<USUARIOS> Unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                Unidad.genericDAL.Add(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestDeleteGeneric()
        {
            USUARIOS x = new USUARIOS();

            using (UnidadDeTrabajo<USUARIOS> Unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(6);
                Unidad.genericDAL.Remove(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateGeneric()
        {
            USUARIOS x;
            
            using (UnidadDeTrabajo<USUARIOS> Unidad = new UnidadDeTrabajo<USUARIOS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(4);
                x.usuario = "pedro";
                Unidad.genericDAL.Update(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }
    }
}
