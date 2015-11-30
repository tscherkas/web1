using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EditFilmModel
    {
        public Film film { get; set; }
        public IEnumerable<WebApplication1.Models.Genre> available_genres { get; set; }
    }
    public class FilmsController : Controller
    {
        private FilmsContext db = new FilmsContext();

        // GET: Films
        public ActionResult Index()
        {
            var i = db.films.ToList();
            return View(db.films.ToList());
        }
        public ActionResult Recommend()
        {
            return View();
        }
        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,date")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            EditFilmModel model = new EditFilmModel() ;
            model.film = film;
            model.available_genres = db.genres.ToList();

            return View(model);
        }

        // POST: Films/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,date")] Film film)
        {
            int number = int.Parse(Request.Params["genre"]);
            if (ModelState.IsValid)
            {
                var genre = (from g in db.genres
                                           where g.ID == number
                             select g).FirstOrDefault<Genre>();
                
                genre.films.Add(film);
                film.genres.Add(genre);
                db.Entry(film).State = EntityState.Modified;
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.films.Find(id);
            db.films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
