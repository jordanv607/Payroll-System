using System;
using BackEnd.DAL;
using BackEnd.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBackEnd
{
    [TestClass]
    public class TestParametros
    {
        [TestMethod]
        public void TestAdd()
        {
            PARAMETROS parametro = new PARAMETROS
            {
                tabla = "PERSONAS",
                campo = "ESTADO",
                descripcion = "INACTIVO"
            };

            using (UnidadDeTrabajo<PARAMETROS> unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                unidad.genericDAL.Add(parametro);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            using (UnidadDeTrabajo<PARAMETROS> Unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                PARAMETROS parametro = Unidad.genericDAL.Get(7);
                parametro.descripcion = "Modificación";
                Unidad.genericDAL.Update(parametro);
                Assert.AreEqual(true, Unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            using (UnidadDeTrabajo<PARAMETROS> Unidad = new UnidadDeTrabajo<PARAMETROS>(new BDContext()))
            {
                PARAMETROS parametro = Unidad.genericDAL.Get(9);
                Unidad.genericDAL.Remove(parametro);
                Assert.AreEqual(true, Unidad.Complete());
            }
        }
    }
}
