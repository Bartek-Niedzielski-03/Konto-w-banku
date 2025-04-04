using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoTests
    {
        [TestMethod]
        public void Konstruktor_UstawiaNazweIBilans()
        {
            var konto = new Konto("Artur Zak", 150);
            Assert.AreEqual("Artur Zak", konto.Nazwa);
            Assert.AreEqual(150, konto.Bilans);
        }

        [TestMethod]
        public void Wplata_ZwiekszaBilans()
        {
            var konto = new Konto("Konrad Marek", 50);
            konto.Wplata(30);
            Assert.AreEqual(80, konto.Bilans);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wplata_ZablokowaneKonto_Wyjatek()
        {
            var konto = new Konto("Ala Kot", 100);
            konto.BlokujKonto();
            konto.Wplata(50);
        }

        [TestMethod]
        public void Wyplata_PrawidlowaZmniejszaBilans()
        {
            var konto = new Konto("Marta Pokorna", 100);
            konto.Wyplata(40);
            Assert.AreEqual(60, konto.Bilans);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_ZablokowaneKonto_Wyjatek()
        {
            var konto = new Konto("Marek Blokada", 100);
            konto.BlokujKonto();
            konto.Wyplata(30);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_ZaDuzo_Wyjatek()
        {
            var konto = new Konto("Jan Wolny", 20);
            konto.Wyplata(50);
        }
    }
}
