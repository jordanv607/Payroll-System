using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.Entities;
using BackEnd.DAL;

namespace TestBackEnd
{
    /// <summary>
    /// Descripción resumida de TestPersonasDAL
    /// </summary>
    [TestClass]
    public class TestPersonasDAL
    {
        public TestPersonasDAL()
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

            PERSONAS x = new PERSONAS
            {
                numeroIdentificacion = "222222",
                nombrePersona = "jordan",
                primerApellido ="jordan",
                segundoApellido = "jordan",
                genero = 1,
                estado = 3,
                nacionalidad = 2,
                correoElectronico = "jordan@gmail.com",
                telefono = "898998"

            };


            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                Unidad.genericDAL.Add(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestDeleteGeneric()
        {
            PERSONAS x = new PERSONAS();

            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(2);
                Unidad.genericDAL.Remove(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateGeneric()
        {
            PERSONAS x;


            using (UnidadDeTrabajo<PERSONAS> Unidad = new UnidadDeTrabajo<PERSONAS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(2);
                x.nombrePersona = "pedro";
                Unidad.genericDAL.Update(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }
    }
}
