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
    public class PostController : Controller
    {

        private CommentsContext cdb = new CommentsContext();
        private HitchhikersContext hdb = new HitchhikersContext();
        private PostsContext pdb = new PostsContext();

        // GET: Post
        public ActionResult Index()
        {
            var posts = from p in pdb.Posts orderby p.Date select p;
            ViewBag.Title = "All Events";
            return View(posts);
        }

        // GET: Post/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Title = "New Event";
            return View();
        }


        // POST: Post/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Producer = User.Identity.Name;
            //    post.ID = post._Content.GetHashCode().ToString();
                if (post.GoogleMap == null)
                    post.GoogleMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15196.733401840413!2d34.76801167151065!3d31.969372704850464!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1502b3f5eb44e1c1%3A0xfbe9b8c6bce59319!2z15TXnteh15zXldecINeU15DXp9eT157XmSDXlNee15vXnNec15Qg15zXnteZ16DXlNec!5e0!3m2!1siw!2sil!4v1501339255742";
                if (post.Photo == null)
                    post.Photo = "https://cdn.dribbble.com/users/22251/screenshots/803201/no-photo-grey.png";

                pdb.Posts.Add(post);
                pdb.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Title = "New Event";
                return View(post);
            }
        }

      

        // GET: Post/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = pdb.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.Name.Equals("Admin") &&
                !User.Identity.Name.Equals(pdb.Posts.Find(id)))
                RedirectToAction("Block", "Home");
            ViewBag.Title = "Edit Event";
            return View(post);

        }

        // POST: Post/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {

            if (ModelState.IsValid)
            {
                if (post.GoogleMap == null)
                    post.GoogleMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15196.733401840413!2d34.76801167151065!3d31.969372704850464!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1502b3f5eb44e1c1%3A0xfbe9b8c6bce59319!2z15TXnteh15zXldecINeU15DXp9eT157XmSDXlNee15vXnNec15Qg15zXnteZ16DXlNec!5e0!3m2!1siw!2sil!4v1501339255742";
                if (post.Photo == null)
                    post.Photo = "https://cdn.dribbble.com/users/22251/screenshots/803201/no-photo-grey.png";

                pdb.Entry(post).State = EntityState.Modified;
                pdb.SaveChanges();
                return RedirectToAction("Index","Users");
            }
            
            RedirectToAction("Index", "Users");
            return View();
        }

        // GET: Post/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = pdb.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Are you sure you want to delete this event?";
            if (!User.Identity.Name.Equals("Admin") &&
               !User.Identity.Name.Equals(pdb.Posts.Find(id)))
                RedirectToAction("Block", "Home");
        

            return View(post);
        }

     

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Post post = pdb.Posts.Find(id);
            post.Comments.Clear();
            pdb.Posts.Remove(post);
            pdb.SaveChanges();

            var controller = DependencyResolver.Current.GetService<Comment>();
            //delete post from reffered photos
            cdb.Comments.RemoveRange(cdb.Comments.Where(c => c.RefferedPost == id));
            cdb.SaveChanges();
            //delete all reffered hitchhikers
            hdb.Hitchhikers.RemoveRange(hdb.Hitchhikers.Where(c => c.PostID == id));
            hdb.SaveChanges();

            return RedirectToAction("Index","Users");

        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = pdb.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            List<Comment> commentList = cdb.Comments.Where(c => c.RefferedPost == id).ToList();

            PostCommentModel pc = new PostCommentModel(){postList = new List<Post>(),commentList = commentList};
            pc.postList.Add(post);
            
            return View(pc);
        }



    }
}