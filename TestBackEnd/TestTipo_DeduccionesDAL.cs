using BackEnd.Entities;
using BackEnd.DAL;
using Xunit.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBackEnd
{
    /// <summary>
    /// Descripción resumida de TestTipo_DeduccionesDAL
    /// </summary>
    [TestClass]
    public class TestTipo_DeduccionesDAL
    {
        public TestTipo_DeduccionesDAL()
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

            TIPO_DEDUCCIONES x = new TIPO_DEDUCCIONES
            {
                nombreDeduccion = "CCSS",
                porcentaje = 8.3M,
                descripcion = "caja"

            };


            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> Unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                Unidad.genericDAL.Add(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestDeleteGeneric()
        {
            TIPO_DEDUCCIONES x = new TIPO_DEDUCCIONES();

            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> Unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(2);
                Unidad.genericDAL.Remove(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateGeneric()
        {
            TIPO_DEDUCCIONES x;


            using (UnidadDeTrabajo<TIPO_DEDUCCIONES> Unidad = new UnidadDeTrabajo<TIPO_DEDUCCIONES>(new BDContext()))
            {
                x = Unidad.genericDAL.Get(2);
                x.descripcion = "modificado";
                Unidad.genericDAL.Update(x);
                Assert.AreEqual(true, Unidad.Complete());
            }

        }
    }
}
