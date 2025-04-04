using System;

namespace Bank
{
    public class KontoLimit
    {
        private Konto konto;
        private decimal limit;
        private bool debetWykorzystany = false;

        public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 0)
        {
            konto = new Konto(klient, bilansNaStart);
            this.limit = limit;
        }

        public string Nazwa => konto.Nazwa;
        public decimal Limit
        {
            get => limit;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limit nie może być ujemny.");
                limit = value;
            }
        }

        public decimal Bilans => konto.Bilans + (debetWykorzystany ? 0 : limit);
        public bool Zablokowane => konto.Zablokowane;

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0.");

            //pozwalamy na wplate nawet jak konto jest zablokowane
            var poleBilans = typeof(Konto).GetField("bilans", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            decimal aktualnyBilans = (decimal)poleBilans.GetValue(konto);
            poleBilans.SetValue(konto, aktualnyBilans + kwota);

            if (aktualnyBilans + kwota > 0 && debetWykorzystany)
            {
                debetWykorzystany = false;
                konto.OdblokujKonto();
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0.");

            if (kwota <= konto.Bilans)
            {
                konto.Wyplata(kwota);
            }
            else if (!debetWykorzystany && kwota <= konto.Bilans + limit)
            {
                konto.Wyplata(konto.Bilans); //wyzeruj
                debetWykorzystany = true;
                konto.BlokujKonto();
            }
            else
            {
                throw new InvalidOperationException("Brak środków lub debet już wykorzystany.");
            }
        }
    }
}
