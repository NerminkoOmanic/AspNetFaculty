using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeDodajUcesnikaVM
    { 
            public int OdjeljenjeStavkaId { get; set; }
            public int TakmicenjeId { get; set; }
            public int Bodovi { get; set; }
            public List<SelectListItem> UceniciList { get; set; }

            public TakmicenjeDodajUcesnikaVM()
            {
                UceniciList = new List<SelectListItem>();
            }

    }
}
