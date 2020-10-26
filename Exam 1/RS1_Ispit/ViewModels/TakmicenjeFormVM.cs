using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakmicenjeFormVM
    {
        public int SkolaId {get; set;}
        public int Razred {get; set;}

        public List<SelectListItem> SkoleList { get; set; }
        public List<SelectListItem> RazrediList { get; set; }

        public TakmicenjeFormVM()
        {
            SkoleList = new List<SelectListItem>();
            RazrediList = new List<SelectListItem>();

            RazrediList.Add(new SelectListItem()
            {
                Text = "Svi razredi",
                Value = "0",
                Selected = true
            });
            
            for (var i = 1; i < 5; i++)
            {
                RazrediList.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
        }

    }
}
