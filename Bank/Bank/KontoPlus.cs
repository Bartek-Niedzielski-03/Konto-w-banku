using System;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal limitDebetu;
        private bool debetWykorzystany = false;

        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limitDebetu = 0)
            : base(klient, bilansNaStart)
        {
            this.limitDebetu = limitDebetu;
        }

        public decimal LimitDebetu
        {
            get => limitDebetu;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Limit nie możebyć ujemny.");
                limitDebetu = value;
            }
        }

        public new decimal Bilans => base.Bilans + (debetWykorzystany ? 0 : limitDebetu);

        public new void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");

            if (kwota <= base.Bilans)
            {
                base.Wyplata(kwota);
            }
            else if (!debetWykorzystany && kwota <= base.Bilans + limitDebetu)
            {
                base.Wyplata(base.Bilans);
                debetWykorzystany = true;
                BlokujKonto();
            }
            else
            {
                throw new InvalidOperationException("Brak środków lub limit debetu już wykorzystany.");
            }
        }

        public new void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0.");

            //umozliwiamy wplate mimo blokady
            var poleBilans = typeof(Konto).GetField("bilans", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            decimal aktualnyBilans = (decimal)poleBilans.GetValue(this);
            poleBilans.SetValue(this, aktualnyBilans + kwota);

            if (aktualnyBilans + kwota > 0 && debetWykorzystany)
            {
                debetWykorzystany = false;
                OdblokujKonto();
            }
        }

    }
}
