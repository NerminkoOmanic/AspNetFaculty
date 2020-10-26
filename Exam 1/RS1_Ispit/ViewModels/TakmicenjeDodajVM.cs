using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeDodajVM
    { 
        public int SkolaId {get; set;}
        public string SkolaDomacin { get; set; }
        public int Razred { get; set; }
        public List<SelectListItem> RazrediList { get;set; }
        public string Predmet { get; set; }
        public List<SelectListItem> PredmetiList { get;set; }

        public DateTime Datum { get; set; }


        public TakmicenjeDodajVM()
        {
            RazrediList = new List<SelectListItem>();
            PredmetiList = new List<SelectListItem>();

            for (int i = 1; i < 5; i++)
            {
                RazrediList.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
        }
      
    }
}
