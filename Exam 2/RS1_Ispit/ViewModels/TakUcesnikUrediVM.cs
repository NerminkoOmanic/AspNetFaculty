using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakUcesnikUrediVM
    {
        public int UcesnikID {get; set;}
        public int Bodovi { get; set; }
        public string NazivUcesnika{get; set;}
    }
}
