using AnimalPartyGallery.Context;
using AnimalPartyGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AnimalPartyGallery.Controllers
{
    public class HomeController : Controller
    {
        private PostsContext db = new PostsContext();
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }


        public ActionResult Weather()
        {
            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Block()
        {

            return View();
        }


    }
}