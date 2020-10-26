using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakDodajVM
    {
        public int SkolaID { get; set; }
        public string SkolaDomacin { get; set; }
        public int Razred {get; set; }
        public string Predmet {get; set; }
        public DateTime Datum { get; set; }
        public List<SelectListItem> PredmetiList {get; set;}
        public List<SelectListItem> RazrediList {get; set;}

        public TakDodajVM()
        {
            PredmetiList = new List<SelectListItem>();
            RazrediList = new List<SelectListItem>();

            for (int i = 1; i < 5; i++)
            {
                RazrediList.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                    Selected = true
                });
            }
        }

    }
}
