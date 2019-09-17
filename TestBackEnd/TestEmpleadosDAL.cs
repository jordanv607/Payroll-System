using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.DAL;
using BackEnd.Entities;

namespace TestBackEnd
{
    /// <summary>
    /// Descripción resumida de TesteEmpleadosDAL
    /// </summary>
    [TestClass]
    public class TestEmpleadosDAL
    {
        public TestEmpleadosDAL()
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

            EMPLEADOS x = new EMPLEADOS
            {
                 idPersona = 1,
                 departamento = 4,
                 puesto = 5,
                 salarioMensual = 20000,
                 portacionArma = 6,
                 numeroCuentaBancaria = "255555",
                 
            };


            using (UnidadDeTrabajo<EMPLEADOS> Unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {               
                Unidad.genericDAL.Add(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestDeleteGeneric()
        {
            EMPLEADOS x = new EMPLEADOS();

            using (UnidadDeTrabajo<EMPLEADOS> Unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(10);
                Unidad.genericDAL.Remove(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateGeneric()
        {
            EMPLEADOS x;


            using (UnidadDeTrabajo<EMPLEADOS> Unidad = new UnidadDeTrabajo<EMPLEADOS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(11);
                x.salarioMensual = 300000;
                Unidad.genericDAL.Update(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }
    }
}
