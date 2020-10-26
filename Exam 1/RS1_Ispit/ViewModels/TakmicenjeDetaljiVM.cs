using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeDetaljiVM
    { 
        public int SkolaId {get; set;}
        public string SkolaDomacin { get; set; }
        public int Razred { get; set; }
        public List<Row> Takmicenja { get; set; }

        public TakmicenjeDetaljiVM()
        {
            Takmicenja = new List<Row>();
        }
        public class Row
        {
            public int TakmicenjeId { get; set; }
            public string Predmet { get; set; }
            public int Razred { get; set; }
            public string DatumTakmicenja { get; set; }
            public int BrojNepristupljenih { get; set; }
            //NajboljiUcesnik
            public string Skola { get; set; }
            public string Odjeljenje { get; set; }
            public string ImePrezime { get; set; }
        }
    }
}
