using AnimalPartyGallery.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;

namespace AnimalPartyGallery.Controllers
{
    public class SearchController : Controller
    {
        private PostsContext postDB = new PostsContext();
        private CommentsContext commentDB = new CommentsContext();
        // GET: Search/Details/5
        public ActionResult EventSearch(string eventName, string eventProducer, string keyWord)
        {
            ViewBag.Title = "Post Search Results";
            var list = postDB.Posts.Where(c => (c.Title.Contains(eventName)&&eventName!="") || (c.Producer.Contains(eventProducer)&&eventProducer!="") ||(c.Content.Contains(keyWord)&&keyWord!=""));
            return View("../Post/index", list.ToList());

        }

        public ActionResult PhotoSearch(string photoTitle, string advertiserName, string keyword)
        {
          
             var list = commentDB.Comments.Where(c => (c.Title.Contains(photoTitle) && photoTitle != "") || (c.Author.Contains(advertiserName)&&advertiserName!="") || (c.Content.Contains(keyword)&&keyword!=""));
           return View("../Comment/IndexSearch",list.ToList());
            
        }




        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
    
        

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



       


     
    }
}
