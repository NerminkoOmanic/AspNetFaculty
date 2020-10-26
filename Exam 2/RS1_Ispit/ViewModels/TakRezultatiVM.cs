using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakRezultatiVM
    {
        public int TakmicenjeID {get; set; }
        public int SkolaID { get; set; }
        public string SkolaDomacin { get; set; }
        public string Predmet { get; set; }
        public int Razred {get; set; }
        public string Datum { get; set; }
        public bool Zakljucano { get; set; }
        public List<Row> Ucesnici{get; set; }

        public TakRezultatiVM()
        {
            Ucesnici = new List<Row>();
        }

        public class Row
        {
            public int TakmicenjeUcesnikID { get; set; }
            public string Odjeljenje { get; set; }
            public int BrojUdnevniku {get; set; }
            public bool Pristupio { get; set; }
            public string PristupioString { get { return Pristupio ? "DA" : "NE"; } }
            public int Bodovi { get; set; }
        }
    }
}
