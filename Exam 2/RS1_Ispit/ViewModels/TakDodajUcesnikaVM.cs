using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakDodajUcesnikaVM
    {
        public int OdjeljenjeStavkaID { get; set; }
        public int Bodovi { get; set; }
        public int TakmicenjeID { get; set; }
        public List<SelectListItem> ListaUcenika { get; set; }

        public TakDodajUcesnikaVM()
        {
            ListaUcenika = new List<SelectListItem>();
        }
    }
}
