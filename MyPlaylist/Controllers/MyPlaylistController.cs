using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPlaylist.EF;
using MyPlaylist.EntityModels;
using MyPlaylist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaylist.Controllers
{
    public class MyPlaylistController : Controller
    {
        private readonly MojContext db;
        public MyPlaylistController(MojContext context) => db = context;
        public IActionResult Index()
        {
            var x = new MyPlaylistIndexVM
            {
                Kategorija = db.Kategorija
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.NazivKategorije
                }).ToList()
            };
            return View(x);
        }
        public IActionResult Odaberi(MyPlaylistIndexVM x)
        {
            var kategorija = db.Kategorija.Find(x.KategorijaId);
            var pjesma = new MyPlaylistOdaberiVM
            {
                KategorijaId = kategorija.Id,
                Kategorija = kategorija.NazivKategorije,
                rows = db.Pjesma
                .Where(s => s.KategorijaId == kategorija.Id)
                .Select(s => new MyPlaylistOdaberiVM.Row
                {
                    PjesmaId = s.Id,
                    NazivPjesme = s.NazivPjesme,
                    NazivIzvodjaca = s.NazivIzvodjaca,
                    Url = s.Url,
                    Ocjena = s.Ocjena,
                    DatumUnosa = s.DatumUnosa.ToString("dd/MM/yyyy"),
                    DatumEditovanja = s.DatumEditovanja.ToString("dd/MM/yyyy"),
                    IsFavorit = s.IsFavorit
                }).ToList()
            };
            return View(pjesma);
        }
        public IActionResult Obrisi(int Id)
        {
            var pjesma = db.Pjesma.Find(Id);
            db.Remove(pjesma);
            db.SaveChanges();

            return Redirect("/MyPlaylist/Index");
        }
        public IActionResult Dodaj(int Id)
        {
            var pjesma = db.Pjesma
                .Include(s => s.Kategorija)
                .Where(s => s.Id == Id)
                .SingleOrDefault();

            var x = new MyPlaylistDodajVM
            {
                PjesmaId = Id,
                KategorijaId = pjesma.KategorijaId,
                Kategorija = pjesma.Kategorija.NazivKategorije

            };
            return View(x);
        }
        public IActionResult Snimi(MyPlaylistDodajVM x)
        {
            Pjesma pjesma;

            if (x.PjesmaId == 0)
            {
                pjesma = new Pjesma();
                db.Add(pjesma);
                TempData["PorukaInfo"] = "Uspjesno ste dodali pjesmu " + pjesma.NazivPjesme;
                /*pjesma = new Pjesma
                {
                    Id = x.PjesmaId,
                    KategorijaId = x.KategorijaId,
                    DatumEditovanja = x.DatumEditovanja,
                    IsFavorit = false,
                    DatumUnosa = x.DatumUnosa,
                    NazivPjesme = x.NazivPjesme,
                    NazivIzvodjaca = x.NazivIzvodjaca,
                    Url = x.Url,
                    Ocjena = x.Ocjena
                };
                db.Add(pjesma);*/

            }
            else 
            {
                pjesma = db.Pjesma.Find(x.PjesmaId);
                TempData["PorukaInfo"] = "Uspjesno ste editovali pjesmu " + pjesma.NazivPjesme;

            }
            pjesma.KategorijaId = x.KategorijaId;
            pjesma.NazivPjesme = x.NazivPjesme;
            pjesma.NazivIzvodjaca = x.NazivIzvodjaca;
            pjesma.Ocjena = x.Ocjena;
            pjesma.Url = x.Url;
            pjesma.IsFavorit = x.IsFavorit;
            pjesma.DatumEditovanja = x.DatumEditovanja;
            pjesma.DatumUnosa = x.DatumUnosa;
            db.SaveChanges();
            return Redirect("/MyPlaylist/Poruka");
        }
        public IActionResult Poruka()
        {
            return View("Poruka");
        }
        public IActionResult Uredi(int Id)
        {
            /*var pjesma = db.Pjesma
                .Include(s => s.Kategorija)
                .Where(s => s.Id == Id)
                .SingleOrDefault();*/

            var pjesma = db.Pjesma.Find(Id);
            /*var x = new MyPlaylistUrediVM
            {
                KategorijaId=pjesma.KategorijaId,
                PjesmaId = pjesma.Id,
                NazivPjesme = pjesma.NazivPjesme,
                NazivIzvodjaca = pjesma.NazivIzvodjaca,
                Ocjena = pjesma.Ocjena,
                Url = pjesma.Url,
                IsFavorit = pjesma.IsFavorit,
                DatumUnosa = pjesma.DatumUnosa,
                DatumEditovanja = pjesma.DatumEditovanja

            };*/
            MyPlaylistDodajVM x;
            if (Id == 0)
            {
                x = new MyPlaylistDodajVM() { };
            }
            else
            {
                x = db.Pjesma
                    .Where(s => s.Id == Id)
                    .Select(s => new MyPlaylistDodajVM
                    {
                        PjesmaId = s.Id,
                        DatumUnosa = s.DatumUnosa,
                        DatumEditovanja = s.DatumEditovanja,
                        NazivIzvodjaca = s.NazivIzvodjaca,
                        KategorijaId = s.KategorijaId,
                        IsFavorit = s.IsFavorit,
                        NazivPjesme = s.NazivPjesme,
                        Ocjena = s.Ocjena,
                        Url = s.Url

                    }).Single();
            
            }
            return View("Uredi",x);
        }
       
    }
}
