using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Films;

namespace WebApplication1.Controllers
{
    public class EditFilmModel
    {
        public Film film { get; set; }
        public IEnumerable<SelectListItem> available_genres { get; set; }
        public IEnumerable<SelectListItem> available_persons { get; set; }
    }
    public class FilmsController : Controller
    {
        private FilmsContext db = new FilmsContext();

        // GET: Films
        public ActionResult Index()
        {
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
            buildGenresItemsList(model);
            buildPersonsItemsList(model);

            return View(model);
        }

        // POST: Films/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,date")] Film film)
        {
            if (ModelState.IsValid)
            {

                int genreID = int.Parse(Request.Params["genre"]);
                int personID = int.Parse(Request.Params["person"]);
                if (genreID != 0)
                {
                    var genre = (from g in db.genres
                                 where g.ID == genreID
                                 select g).FirstOrDefault<Genre>();
                    genre.films.Add(film);
                    film.genres.Add(genre);
                    db.Entry(film).State = EntityState.Modified;
                    db.Entry(genre).State = EntityState.Modified;
                }
                if (personID != 0)
                {
                    var person = (from p in db.persons
                                  where p.ID == personID
                                  select p).FirstOrDefault<Person>();
                    person.films.Add(film);
                    film.persons.Add(person);
                    db.Entry(film).State = EntityState.Modified;
                    db.Entry(person).State = EntityState.Modified;
                }

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
        protected void buildGenresItemsList(EditFilmModel model)
        {

            var available_genres = new List<Genre>();
            available_genres.Add(
                new Genre()
                {
                    ID = 0,
                    name = ""
                }
                );
            var listGenresToSelect = available_genres.Concat(db.genres.ToList());
            model.available_genres = listGenresToSelect.Select(x => new SelectListItem
            {
                Text = x.name,
                Value = x.ID.ToString()
            });
        }
        protected void buildPersonsItemsList(EditFilmModel model)
        {

            var available_persons = new List<Person>();
            available_persons.Add(
                new Person()
                {
                    ID = 0,
                    name = ""
                }
                );
            var listPersonsToSelect = available_persons.Concat(db.persons.ToList());
            model.available_persons = listPersonsToSelect.Select(x => new SelectListItem
            {
                Text = x.name,
                Value = x.ID.ToString()
            });
        }
    }
}
