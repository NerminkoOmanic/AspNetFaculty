using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class TakmicenjeController : Controller
    {
        private MojContext _db;

        public TakmicenjeController(MojContext context)
        {
            _db = context;
        }
        // GET: TakmicenjeController
        public ActionResult Index()
        {
            TakIndexVM model = new TakIndexVM();
            model.SkoleList = _db.Skola.Select(x => new SelectListItem()
            {
                Value=x.Id.ToString(),
                Text = x.Naziv
            }).ToList();
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Index(TakIndexVM model)
        //{
        //    return RedirectToAction(nameof(Details), new { model.SkolaID, model.Razred });
        //}
        public IActionResult DetailsPV(int skolaId,int razred)
        {
            TakDetailsVM model = new TakDetailsVM()
            {
                Razred = razred,
                SkolaID = skolaId,
                SkolaDomacin = _db.Skola.Where(x=>x.Id==skolaId).Select(x=>x.Naziv).FirstOrDefault()
            };

            if (razred==0)
            {
                model.Takmicenja = _db.Takmicenje.Where(x => x.SkolaID == skolaId)
                    .Select(x => new TakDetailsVM.Row()
                    {
                        TakmicenjeID =  x.TakmicenjeID,
                        Predmet = x.Predmet.Naziv,
                        Razred = x.Razred,
                        Datum = x.Datum.ToString("dd.MM.yyyy"),
                        BrojNepristupljenih = x.Ucesnici.Where(y=>y.Pristupio==false).Count(),
                        //najbolji ucesnik
                        UcesnikSkola = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).First(),
                        UcesnikOdjeljenje = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Oznaka).First(),
                        UcesnikIme = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Ucenik.ImePrezime).First()

                    }).ToList();
            }
            else
            {
                model.Takmicenja = _db.Takmicenje.Where(x => x.SkolaID == skolaId && x.Razred == razred)
                    .Select(x => new TakDetailsVM.Row()
                    {
                        TakmicenjeID = x.TakmicenjeID,
                        Predmet = x.Predmet.Naziv,
                        Razred = x.Razred,
                        Datum = x.Datum.ToString("dd.MM.yyyy"),
                        BrojNepristupljenih = x.Ucesnici.Where(y => y.Pristupio == false).Count(),
                        //najbolji ucesnik
                        UcesnikSkola = x.Ucesnici.OrderByDescending(y => y.Bodovi)
                            .Select(y => y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).First(),
                        UcesnikOdjeljenje = x.Ucesnici.OrderByDescending(y => y.Bodovi)
                            .Select(y => y.OdjeljenjeStavka.Odjeljenje.Oznaka).First(),
                        UcesnikIme = x.Ucesnici.OrderByDescending(y => y.Bodovi)
                            .Select(y => y.OdjeljenjeStavka.Ucenik.ImePrezime).First()

                    }).ToList();
            }
            return PartialView(model);
        }
        // GET: TakmicenjeController/Details/5
        public ActionResult Details(int skolaId, int razred)
        {
            TakDetailsVM model = new TakDetailsVM()
            {
                Razred = razred,
                SkolaID = skolaId,
                SkolaDomacin = _db.Skola.Where(x=>x.Id==skolaId).Select(x=>x.Naziv).FirstOrDefault()
            };

            if (razred==0)
            {
                model.Takmicenja = _db.Takmicenje.Where(x => x.SkolaID == skolaId)
                    .Select(x => new TakDetailsVM.Row()
                    {
                        TakmicenjeID =  x.TakmicenjeID,
                        Predmet = x.Predmet.Naziv,
                        Razred = x.Razred,
                        Datum = x.Datum.ToString("dd.MM.yyyy"),
                        BrojNepristupljenih = x.Ucesnici.Where(y=>y.Pristupio==false).Count(),
                        //najbolji ucesnik
                        UcesnikSkola = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).First(),
                        UcesnikOdjeljenje = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Oznaka).First(),
                        UcesnikIme = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Ucenik.ImePrezime).First()

                    }).ToList();
            }
            else
            {
                model.Takmicenja = _db.Takmicenje.Where(x => x.SkolaID == skolaId && x.Razred == razred)
                    .Select(x => new TakDetailsVM.Row()
                    {
                        TakmicenjeID = x.TakmicenjeID,
                        Predmet = x.Predmet.Naziv,
                        Razred = x.Razred,
                        Datum = x.Datum.ToString("dd.MM.yyyy"),
                        BrojNepristupljenih = x.Ucesnici.Where(y => y.Pristupio == false).Count(),
                        //najbolji ucesnik
                        UcesnikSkola = x.Ucesnici.OrderByDescending(y => y.Bodovi)
                            .Select(y => y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).First(),
                        UcesnikOdjeljenje = x.Ucesnici.OrderByDescending(y => y.Bodovi)
                            .Select(y => y.OdjeljenjeStavka.Odjeljenje.Oznaka).First(),
                        UcesnikIme = x.Ucesnici.OrderByDescending(y => y.Bodovi)
                            .Select(y => y.OdjeljenjeStavka.Ucenik.ImePrezime).First()

                    }).ToList();
            }
            return View(model);
        }

        // GET: TakmicenjeController/Create
        public ActionResult Dodaj(int skolaID)
        {
            TakDodajVM model = new TakDodajVM()
            {
                SkolaID = skolaID,
                SkolaDomacin = _db.Skola.Where(x=>x.Id == skolaID).Select(x=>x.Naziv).FirstOrDefault()
            };
            model.PredmetiList = _db.Predmet.GroupBy(x => x.Naziv).Select(x=>x.First()).Select(x => new SelectListItem()
            {
                Value = x.Naziv,
                Text = x.Naziv
            }).ToList();
            return View(model);
        }

        // POST: TakmicenjeController/Create
        [HttpPost]
        public ActionResult Dodaj(TakDodajVM model)
        {
            Predmet p = _db.Predmet.Where(x => x.Naziv == model.Predmet && x.Razred==model.Razred).FirstOrDefault();
            Takmicenje t =new Takmicenje()
                {
                    SkolaID = model.SkolaID,
                    PredmetID = p.Id,
                    Razred = model.Razred,
                    Datum = model.Datum,
                    Zakljucano = false
                };
                List<TakmicenjeUcesnik> temp = _db.DodjeljenPredmet
                    .Where(x => x.Predmet.Id == p.Id && x.ZakljucnoKrajGodine==5)
                    .Select(x => new TakmicenjeUcesnik()
                    {
                        OdjeljenjeStavkaID = x.OdjeljenjeStavkaId,
                        Bodovi = 0,
                        Pristupio = false
                    }).ToList();

                t.Ucesnici = new List<TakmicenjeUcesnik>();
                foreach (var item in temp)
                {
                    if (_db.DodjeljenPredmet
                        .Where(x => x.OdjeljenjeStavkaId == item.OdjeljenjeStavkaID && x.Predmet.Razred == t.Razred)
                        .Select(x => x.ZakljucnoKrajGodine).Average() >= 4)
                    {
                        t.Ucesnici.Add(item);
                    }    

                }

                _db.Add(t);
                _db.SaveChanges();
                
                return RedirectToAction(nameof(Index));

        }

        public IActionResult Rezultati(int takmicenjeID)
        {
            TakRezultatiVM model = _db.Takmicenje.Where(x => x.TakmicenjeID == takmicenjeID)
                .Select(x => new TakRezultatiVM()
                {
                    TakmicenjeID = x.TakmicenjeID,
                    SkolaDomacin = x.Skola.Naziv,
                    SkolaID = x.SkolaID,
                    Predmet = x.Predmet.Naziv,
                    Razred = x.Razred,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Zakljucano = x.Zakljucano
                }).FirstOrDefault();
            return View(model);
        }

        public IActionResult RezultatiUcesniciPV(int takmicenjeID)
        {
            TakRezultatiVM model = new TakRezultatiVM();
            model.TakmicenjeID = takmicenjeID;
            model.Zakljucano = _db.Takmicenje.Where(x => x.TakmicenjeID == takmicenjeID).Select(x => x.Zakljucano)
                .FirstOrDefault();
            model.Ucesnici = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeID == takmicenjeID)
                .Select(x => new TakRezultatiVM.Row()
                {
                    TakmicenjeUcesnikID = x.TakmicenjeUcesnikID,
                    Odjeljenje = x.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    BrojUdnevniku = x.OdjeljenjeStavka.BrojUDnevniku,
                    Bodovi = x.Bodovi,
                    Pristupio = x.Pristupio
                }).ToList();
            return PartialView(model);
        }

        public IActionResult Zakljucaj(int takmicenjeID)
        {
            Takmicenje t = _db.Takmicenje.Where(x => x.TakmicenjeID == takmicenjeID).FirstOrDefault();
            if (t != null)
            {
                t.Zakljucano = true;
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Rezultati), new {takmicenjeID});
        }
        
        public ActionResult Pristupio(int ucesnikID)
        {
            TakmicenjeUcesnik tu = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikID == ucesnikID)
                .FirstOrDefault();
            if (tu != null)
            {
                tu.Pristupio = !tu.Pristupio;
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Rezultati), new {tu.TakmicenjeID});
        }

        public IActionResult Bodovi(int ucesnikID, int bodovi)
        {
            TakmicenjeUcesnik tu = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikID == ucesnikID)
                .FirstOrDefault();

            if (tu != null)
            {
                tu.Bodovi = bodovi;
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Rezultati),new{tu.TakmicenjeID});
        }

        public IActionResult UrediUcesnika(int ucesnikID)
        {
            TakUcesnikUrediVM model = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikID == ucesnikID)
                .Select(x => new TakUcesnikUrediVM()
                {
                    UcesnikID = x.TakmicenjeUcesnikID,
                    NazivUcesnika = x.OdjeljenjeStavka.Odjeljenje.Oznaka + "  |  " + x.OdjeljenjeStavka.Ucenik.ImePrezime,
                    Bodovi = x.Bodovi
                }).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public IActionResult UrediUcesnika(TakUcesnikUrediVM model)
        {
            TakmicenjeUcesnik tu = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikID == model.UcesnikID)
                .FirstOrDefault();
            if (tu != null)
            {
                tu.Bodovi = model.Bodovi;
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Rezultati), new {tu.TakmicenjeID});
        }

        public IActionResult DodajUcesnikPV(int takmicenjeID)
        {
            TakDodajUcesnikaVM model = new TakDodajUcesnikaVM();
            model.TakmicenjeID = takmicenjeID;
            Takmicenje t = _db.Takmicenje.Where(x => x.TakmicenjeID == takmicenjeID).FirstOrDefault();
            //lista svih ucenika koji idu u razred za koji je napravljeno odredjeno takmicenje
            List<OdjeljenjeStavka> sviUceniciList = _db.OdjeljenjeStavka
                .Where(x => x.Odjeljenje.Razred == t.Razred)
                .Include(x=>x.Odjeljenje)
                .Include(x=>x.Ucenik)
                .ToList();
            //lista ucenika koji su vec registrirani za takmicenje
            List<OdjeljenjeStavka> takUcesniciList = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeID == takmicenjeID)
                .Select(x => new OdjeljenjeStavka()
                {
                    Id = x.OdjeljenjeStavkaID
                }).ToList();
            foreach (var itemSvi in sviUceniciList)
            {
                bool pronadjen = false;
                foreach (var itemUcesnici in takUcesniciList)
                {
                    if (itemSvi.Id == itemUcesnici.Id)
                    {
                        pronadjen = true;
                    }

                }

                if (!pronadjen)
                {
                    model.ListaUcenika.Add(new SelectListItem()
                    {
                        Text = itemSvi.Odjeljenje.Oznaka + " | " + itemSvi.Ucenik.ImePrezime,
                        Value = itemSvi.Id.ToString()
                    });
                }
            }

            return PartialView(model);

        }

        [HttpPost]
        public IActionResult DodajUcesnikPV(TakDodajUcesnikaVM model)
        {
            Takmicenje t = _db.Takmicenje.Where(x => x.TakmicenjeID == model.TakmicenjeID).FirstOrDefault();
            bool pristup = false;
            if (model.Bodovi > 0)
            {
                pristup = true;
            }
            
            if (t != null)
            {
                t.Ucesnici.Add(new TakmicenjeUcesnik()
                {
                    OdjeljenjeStavkaID = model.OdjeljenjeStavkaID,
                    Bodovi = model.Bodovi,
                    Pristupio = pristup
                });
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Rezultati), new {model.TakmicenjeID});

        }
        //// GET: TakmicenjeController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: TakmicenjeController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: TakmicenjeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TakmicenjeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
