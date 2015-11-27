using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FilmsRecommendationsController : Controller
    {
        // GET: FilmsRecommendations

        private FilmsContext db = new FilmsContext();
        public ActionResult Index()
        {
            return View(db.genres.ToList());
        }

        public ActionResult Recommend()
        {
            var recommendation = new Recommendation
            {
                GenreName = Request.Params["genre"]
            };
            return View(recommendation);
        }
    }
}