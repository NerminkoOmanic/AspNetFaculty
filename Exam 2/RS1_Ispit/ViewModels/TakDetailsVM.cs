using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakDetailsVM
    {
        public int SkolaID { get; set; }
        public string SkolaDomacin { get; set; }
        public int Razred {get; set; }
        public List<Row> Takmicenja{get; set; }

        public TakDetailsVM()
        {
            Takmicenja = new List<Row>();
        }

        public class Row
        {
            public int TakmicenjeID { get; set; }
            public string Predmet { get; set; }
            public int Razred { get; set; }
            public string Datum { get; set; }
            public int BrojNepristupljenih { get; set; }
            //najbolji ucesnik
            public string UcesnikSkola { get; set; }
            public string UcesnikOdjeljenje {get; set;}
            public string UcesnikIme {get; set;}
        }
    }
}
