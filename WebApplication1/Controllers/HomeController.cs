using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Image()
        {
            var model = new ImagesModel();
            model.ImagesURIs.Add(new Uri("http://content.onliner.by/news/realt/2012/04/iXRO-8S46Y2.jpg"));
            model.ImagesURIs.Add(new Uri("http://content.onliner.by/news/realt/2012/04/yaOJz5wwLo2.jpg"));
            model.ImagesURIs.Add(new Uri("http://content.onliner.by/news/realt/2012/04/iXRO-8S46Y1.jpg"));

            return View(model);
        }
        public ActionResult Definition()
        {
            return View();
        }
        public ActionResult Comments()
        {
            var model = Session["comments"] as List<CommentModel>;
            if (model == null)
            {
                model = new List<CommentModel>();
            }
            return View(model);
        }
        public ActionResult CommentForm()
        {
            CommentModel model = new CommentModel();
            model.Author = "dslhl";
            return View(model);
        }
        public ActionResult AddComment(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                var comments = Session["comments"] as List<CommentModel>;
                if (comments == null)
                {
                    comments = new List<CommentModel>();
                }
                comments.Add(model);
                Session["comments"] = comments;

                return RedirectToAction("Comments");
            }
            else
                return View("CommentForm", model);
        }
        public ActionResult Music()
        {
            var model = new MusicModel();
            model.creationDate = new DateTime(1999, 10, 10);
            return View(model);
        }
    }
}