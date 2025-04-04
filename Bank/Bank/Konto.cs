using System;

namespace Bank
{
    public class Konto
    {
        private string klient;
        private decimal bilans;
        private bool zablokowane = false;

        //prywatny konstr
        private Konto() { }

        //konstruktor główny
        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.klient = klient ?? throw new ArgumentNullException(nameof(klient));
            if (bilansNaStart < 0)
                throw new ArgumentException("Bilans nie może być ujemny.");
            bilans = bilansNaStart;
        }

        //własciowosci
        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public void Wplata(decimal kwota)
        {
            if (zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0.");
            bilans += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0.");
            if (kwota > bilans)
                throw new InvalidOperationException("Brak wystarczających środków.");
            bilans -= kwota;
        }

        public void BlokujKonto()
        {
            zablokowane = true;
        }

        public void OdblokujKonto()
        {
            zablokowane = false;
        }
    }
}
