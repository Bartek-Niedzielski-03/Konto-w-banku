using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoLimitTests
    {
        [TestMethod]
        public void Wyplata_WDebecie_ZablokowaneKonto()
        {
            var konto = new KontoLimit("Michał Tomczyk", 50, 100);
            konto.Wyplata(120); // 50 z bilansu + 70 debetu
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Wplata_PoDebecie_OdblokowujeKonto()
        {
            var konto = new KontoLimit("Marta Dudek", 50, 100);
            konto.Wyplata(120);
            konto.Wplata(80); // bilans > 0
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DrugiDebet_Niedozwolony()
        {
            var konto = new KontoLimit("Robert Lewy", 50, 100);
            konto.Wyplata(120);
            konto.Wyplata(10); // drugi debet – powinien rzucić wyjątek
        }
    }
}
