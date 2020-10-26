using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class TakIndexVM
    {
        public int SkolaID {get; set;}
        public int Razred { get; set; }
        public List<SelectListItem> SkoleList {get; set;}
        public List<SelectListItem> RazrediList {get; set;}

        public TakIndexVM()
        {
            SkoleList = new List<SelectListItem>();
            RazrediList = new List<SelectListItem>();


            RazrediList.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Svi razredi",
                Selected = true
            });
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
