using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoPlusTests
    {
        [TestMethod]
        public void Wyplata_WDebecie_ZablokowaneKonto()
        {
            var konto = new KontoPlus("Aleksandra Kowal", 50, 100);
            konto.Wyplata(100); 
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Wplata_PoDebecie_OdblokowujeKonto()
        {
            var konto = new KontoPlus("Jakub Polak", 50, 100);
            konto.Wyplata(100);
            konto.Wplata(60); 
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_DrugiDebet_Niedozwolony()
        {
            var konto = new KontoPlus("Karolina Polakowska", 50, 100);
            konto.Wyplata(100); 
            konto.Wyplata(10); //wyjatek
        }

    }
}
