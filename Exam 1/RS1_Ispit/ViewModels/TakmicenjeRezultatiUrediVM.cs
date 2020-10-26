using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeRezultatiUrediVM
    { 
            public string UcenikOznaka { get; set; }
            public int TakmicenjeUcesnikId{get; set;}
            public int Bodovi { get; set; }

        
    }
}
