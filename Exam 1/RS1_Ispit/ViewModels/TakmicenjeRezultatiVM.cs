using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeRezultatiVM
    { 
        public int TakmicenjeId {get; set; }
        public int SkolaId {get; set;}
        public string SkolaDomacin { get; set; }
        public string Predmet { get; set; }
        public string Datum { get; set; }
        public int Razred { get; set; }
        public bool Zakljucano {get;set;}
        public List<Row> Ucesnici { get; set; }

        public TakmicenjeRezultatiVM()
        {
            Ucesnici = new List<Row>();
        }
        public class Row
        {
            public int TakmicenjeUcesnikId{get; set;}
            public string Odjeljenje { get; set; }
            public int BrojUDnevniku { get; set; }
            public bool Pristupio { get; set; }
            public string Pristupiostring {get {return Pristupio ? "DA" : "NE"; }}
            public int Bodovi { get; set; }
            
        }
    }
}
