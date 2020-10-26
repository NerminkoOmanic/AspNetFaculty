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
            TakmicenjeFormVM model = new TakmicenjeFormVM();
            model.SkoleList = _db.Skola.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(TakmicenjeFormVM model)
        {
          
            return RedirectToAction(nameof(Detalji), new { skolaId = model.SkolaId, razred = model.Razred });
        }

        // GET: TakmicenjeController/Details/5
        public ActionResult Detalji(int skolaId, int razred)
        {
            TakmicenjeDetaljiVM model = _db.Skola.Where(x => x.Id == skolaId).Select(y => new TakmicenjeDetaljiVM()
            {
                Razred = razred,
                SkolaDomacin = y.Naziv,
                SkolaId = y.Id
            }).FirstOrDefault();
            if (razred == 0)
            {
                model.Takmicenja = _db.Takmicenje.Where(x => x.Skola.Id == skolaId).Select(x =>
                    new TakmicenjeDetaljiVM.Row()
                    {
                        TakmicenjeId =  x.TakmicenjeId,
                        Predmet = x.Predmet.Naziv,
                        Razred = x.Razred,
                        DatumTakmicenja = x.Datum.ToString("dd.MM.yyyy"),
                        BrojNepristupljenih = x.Ucesnici.Where(y=>y.Pristupio==false).Count(),
                        //najbolji ucesnik
                        Skola = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).FirstOrDefault(),
                        Odjeljenje = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Oznaka).FirstOrDefault(),
                        ImePrezime = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Ucenik.ImePrezime).FirstOrDefault()
                    }).ToList();
            }
            else
            {
                model.Takmicenja = _db.Takmicenje.Where(x => x.Skola.Id == skolaId && x.Razred == razred).Select(x =>
                    new TakmicenjeDetaljiVM.Row()
                    {
                        TakmicenjeId =  x.TakmicenjeId,
                        Predmet = x.Predmet.Naziv,
                        Razred = x.Razred,
                        DatumTakmicenja = x.Datum.ToString("dd.MM.yyyy"),
                        BrojNepristupljenih = x.Ucesnici.Where(y=>y.Pristupio==false).Count(),
                        //najbolji ucesnik
                        Skola = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Skola.Naziv).FirstOrDefault(),
                        Odjeljenje = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Odjeljenje.Oznaka).FirstOrDefault(),
                        ImePrezime = x.Ucesnici.OrderByDescending(y=>y.Bodovi).Select(y=>y.OdjeljenjeStavka.Ucenik.ImePrezime).FirstOrDefault()
                    }).ToList();
            }
            return View(nameof(Detalji),model);
        }

        // GET: TakmicenjeController/Create
        public ActionResult Dodaj(int skolaId)
        {
            TakmicenjeDodajVM model = new TakmicenjeDodajVM();
            model.SkolaDomacin = _db.Skola.Where(x => x.Id == skolaId).Select(x => x.Naziv).FirstOrDefault();
            model.SkolaId = skolaId;

            model.PredmetiList = _db.Predmet.GroupBy(x => x.Naziv).Select(x => x.First()).Select(x =>
                new SelectListItem()
                {
                    Text = x.Naziv,
                    Value = x.Naziv.ToString()
                }).ToList();
            
            return View(nameof(Dodaj),model);
        }

        // POST: TakmicenjeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dodaj(TakmicenjeDodajVM model)
        {
            Predmet p = _db.Predmet.Where(x => x.Naziv == model.Predmet && x.Razred == model.Razred).FirstOrDefault();
            Takmicenje t = new Takmicenje()
            {
                SkolaId = model.SkolaId,
                PredmetId = p.Id,
                Datum = model.Datum,
                Razred = model.Razred,
                Zakljuceno = false
            };
            List<TakmicenjeUcesnik> temp = new List<TakmicenjeUcesnik>();

            temp = _db.DodjeljenPredmet.Where(x => x.PredmetId == p.Id && x.ZakljucnoKrajGodine == 5).Select(x =>
                new TakmicenjeUcesnik()
                {
                    OdjeljenjeStavkaId = x.OdjeljenjeStavkaId,
                    Pristupio = false,
                    Bodovi = 0
                }).ToList();

            t.Ucesnici = new List<TakmicenjeUcesnik>();

            foreach (var item in temp)
            {
                if (_db.DodjeljenPredmet.Where(x => x.OdjeljenjeStavkaId == item.OdjeljenjeStavkaId
                                                    && x.Predmet.Razred == t.Razred).Select(x => x.ZakljucnoKrajGodine)
                    .Average() >= 4)
                {
                    t.Ucesnici.Add(item);
                }
            }

            _db.Add(t);
            _db.SaveChanges();

            return RedirectToAction(nameof(Detalji),new { skolaId = model.SkolaId});
        }

        // GET: TakmicenjeController/Edit/5
        public ActionResult Rezultati(int takmicenjeId)
        {
            TakmicenjeRezultatiVM model = _db.Takmicenje.Where(x => x.TakmicenjeId == takmicenjeId).Select(x =>
                new TakmicenjeRezultatiVM()
                {
                    TakmicenjeId = x.TakmicenjeId,
                    SkolaDomacin = x.Skola.Naziv,
                    SkolaId = x.SkolaId,
                    Predmet = x.Predmet.Naziv,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Razred = x.Razred,
                    Zakljucano = x.Zakljuceno
                }).FirstOrDefault();
            return View(model);
        }

        public IActionResult RezultatiPV(int takmicenjeId)
        {
            TakmicenjeRezultatiVM model = new TakmicenjeRezultatiVM();
            model.TakmicenjeId = takmicenjeId;
            model.Zakljucano = _db.Takmicenje.Where(x => x.TakmicenjeId == takmicenjeId).Select(x => x.Zakljuceno)
                .FirstOrDefault();
            model.Ucesnici = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeId == takmicenjeId).Select(x =>
                new TakmicenjeRezultatiVM.Row()
                {
                    TakmicenjeUcesnikId = x.TakmicenjeUcesnikId,
                    Odjeljenje = x.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    Pristupio = x.Pristupio,
                    Bodovi = x.Bodovi,
                    BrojUDnevniku = x.OdjeljenjeStavka.BrojUDnevniku
                }).ToList();
            return PartialView(model);
        }

        public ActionResult Zakljucaj(int takmicenjeId)
        {
            Takmicenje t = _db.Takmicenje.Where(x => x.TakmicenjeId == takmicenjeId).FirstOrDefault();
            if (t != null)
            {
                t.Zakljuceno = true;

            }
            _db.SaveChanges();

            return RedirectToAction(nameof(Rezultati), new{takmicenjeId});
        }

        public ActionResult Prisustvo(int ucesnikId)
        {
            TakmicenjeUcesnik ucesnik =
                _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikId == ucesnikId).FirstOrDefault();

            if (ucesnik!=null)
            {
                ucesnik.Pristupio = !ucesnik.Pristupio;
            }

            _db.SaveChanges();

            return RedirectToAction(nameof(RezultatiPV),new{ucesnik.TakmicenjeId});
        }

        public IActionResult UrediPV(int ucesnikId)
        {
            TakmicenjeRezultatiUrediVM model = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeUcesnikId == ucesnikId)
                .Select(x => new TakmicenjeRezultatiUrediVM()
                {
                    UcenikOznaka = x.OdjeljenjeStavka.Odjeljenje.Oznaka + " - " + x.OdjeljenjeStavka.Ucenik.ImePrezime,
                    Bodovi = x.Bodovi,
                    TakmicenjeUcesnikId = x.TakmicenjeUcesnikId
                }).FirstOrDefault();
            return PartialView( model);
        }

        [HttpPost]
        public IActionResult UrediPV(int takmicenjeUcesnikId, int bodovi)
        {
            TakmicenjeUcesnik ucesnik = _db.TakmicenjeUcesnik
                .Where(x => x.TakmicenjeUcesnikId == takmicenjeUcesnikId).FirstOrDefault();
            if (bodovi > 0 && bodovi < 101)
            {
                ucesnik.Bodovi = bodovi;
                if (!ucesnik.Pristupio)
                {
                    ucesnik.Pristupio = true;
                }
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(RezultatiPV), new {ucesnik.TakmicenjeId});
        }

        public IActionResult DodajUcesnikaPV(int takmicenjeId)
        {
            TakmicenjeDodajUcesnikaVM model = new TakmicenjeDodajUcesnikaVM();
            List<OdjeljenjeStavka> temp = _db.OdjeljenjeStavka.Include(x=>x.Ucenik).Include(x=>x.Odjeljenje).ToList();
            List<TakmicenjeUcesnik> ucesnici = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeId == takmicenjeId).Select(
                x => new TakmicenjeUcesnik()
                {
                    TakmicenjeId = takmicenjeId,
                    OdjeljenjeStavkaId = x.OdjeljenjeStavkaId
                }).ToList();

            foreach (var itemOdjeljenjeStavka in temp)
            {
                bool found = false;
                foreach (var itemTakmicenjeUcesnik in ucesnici)
                {
                    if (itemOdjeljenjeStavka.Id == itemTakmicenjeUcesnik.OdjeljenjeStavkaId)
                    {
                         found = true;
                    }
                }

                if (!found)
                {

                    model.UceniciList.Add(new SelectListItem()
                    {
                        Value = itemOdjeljenjeStavka.Id.ToString(),
                        Text = itemOdjeljenjeStavka.Odjeljenje.Oznaka + " - " + itemOdjeljenjeStavka.Ucenik.ImePrezime
                    });
                }
            }

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DodajUcesnikaPV(TakmicenjeDodajUcesnikaVM model)
        {
            TakmicenjeUcesnik ucesnik = new TakmicenjeUcesnik()
            {
                OdjeljenjeStavkaId = model.OdjeljenjeStavkaId,
                Bodovi = model.Bodovi
            };
            if (model.Bodovi > 0)
            {
                ucesnik.Pristupio = true;
            }

            Takmicenje t = _db.Takmicenje.Where(x => x.TakmicenjeId == model.TakmicenjeId).FirstOrDefault();
            t.Ucesnici.Add(ucesnik);
            _db.SaveChanges();
            return RedirectToAction(nameof(RezultatiPV), new {model.TakmicenjeId});
        }



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
