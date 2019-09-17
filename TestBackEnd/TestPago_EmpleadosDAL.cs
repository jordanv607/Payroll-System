using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.Entities;
using BackEnd.DAL;

namespace TestBackEnd
{
    /// <summary>
    /// Descripción resumida de TestPago_EmpleadosDAL
    /// </summary>
    [TestClass]
    public class TestPago_EmpleadosDAL
    {
        public TestPago_EmpleadosDAL()
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

            PAGO_EMPLEADOS x = new PAGO_EMPLEADOS
            {
                idEmpleado = 1,
                salarioBruto = 2000,
                totalDeducciones = 2000,
                salarioNeto = 2222,
                fechaPago = DateTime.Now.Date
            };


            using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
            {
                Unidad.genericDAL.Add(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestDeleteGeneric()
        {
            PAGO_EMPLEADOS x = new PAGO_EMPLEADOS();

            using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(2);
                Unidad.genericDAL.Remove(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateGeneric()
        {
            PAGO_EMPLEADOS x;


            using (UnidadDeTrabajo<PAGO_EMPLEADOS> Unidad = new UnidadDeTrabajo<PAGO_EMPLEADOS>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(3);
                x.salarioBruto = 300000;
                Unidad.genericDAL.Update(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        //#1
        [TestMethod]
        public void TestCalculoSalarioNeto()
        {
            CalculoPlanilla c = new CalculoPlanilla();
            Assert.AreEqual(true, c.calcularSalarioNeto());
        }

        //#2
        [TestMethod]
        public void TestCalculoDeducciones()
        {
            CalculoPlanilla c = new CalculoPlanilla();
            Assert.AreEqual(true, c.calcularDeducciones());
        }

        //#3
        [TestMethod]
        public void TestCalculoSalarioBruto()
        {
            CalculoPlanilla c = new CalculoPlanilla();
            Assert.AreEqual(true, c.calcularSalarioBruto());

        }

        [TestMethod]
        public void TestactualizarLista()
        {
            CalculoPlanilla c = new CalculoPlanilla();
           // Assert.AreEqual(true, c.actualizarLista());

        }

        [TestMethod]
        public void TestgetEmpleadosActivos()
        {
            IPagoEmpleado pE = new PagoEmleadoDALImpl();
            pE.getEmpleadosActivos();

        }

        [TestMethod]
        public void TestSalarioNeto()
        {
            IPagoEmpleado pE = new PagoEmleadoDALImpl();
            DateTime date = new DateTime(2011, 6, 10);
            pE.calcularSalarioNeto(date);

        }
    }
}


