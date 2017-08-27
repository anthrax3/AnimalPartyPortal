using AnimalPartyGallery.Context;
using AnimalPartyGallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AnimalPartyGallery.Controllers
{
    public class HitchhikerController : Controller
    {
        private CommentsContext cdb = new CommentsContext();
        private HitchhikersContext hdb = new HitchhikersContext();
        private PostsContext pdb = new PostsContext();


        // GET: Hitchhiker
        public ActionResult Index(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = pdb.Posts.Find(id);
            if (post == null)
                return HttpNotFound();

            ViewBag.EventName = post.Title;
            ViewBag.EventDate = post.Date;
            return View(hdb.Hitchhikers.Where(c=>c.PostID==id).ToList());
        }

        // GET: Hitchhiker/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hitchhiker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hitchhiker/Create
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

        // GET: Hitchhiker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hitchhiker/Edit/5
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

     

        // GET: Hitchhiker/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hitchhiker/Delete/5
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
