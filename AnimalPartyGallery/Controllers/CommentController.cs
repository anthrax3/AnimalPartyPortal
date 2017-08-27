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
    public class CommentController : Controller
    {
    
        private CommentsContext cdb = new CommentsContext();
        private HitchhikersContext hdb = new HitchhikersContext();
        private PostsContext pdb = new PostsContext();

        // GET: Comment/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return HttpNotFound();
            Post post = pdb.Posts.Find(id);
            if (post != null)
            {
                Comment comment = new Comment();
                comment.RefferedPost = post.ID;
                return View(comment);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Comment/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Author = User.Identity.Name;
                cdb.Comments.Add(comment);
                cdb.SaveChanges();

                Post post= pdb.Posts.Find(comment.RefferedPost);
                IntegerData idata = new IntegerData();
                idata.Data = comment.ID;
                post.Comments.Add(idata);

                pdb.Entry(post).State = EntityState.Modified;
                pdb.SaveChanges();
               
                

                //if is hitchhiker
                if (comment.Hitchhiker == true)
                {
                    Hitchhiker hitch = new Hitchhiker() { Name = comment.Author, Phone = comment.PhoneNumber, PostID = comment.RefferedPost };
                    hdb.Hitchhikers.Add(hitch);
                    hdb.SaveChanges();

                }
                return RedirectToAction("Details", "Post", new { id = post.ID });
            }
            else
                return View(comment);
        }



        //GET: Comment/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Comment comment = cdb.Comments.Find(id);
            if (comment == null)
                return HttpNotFound();

            if (!User.Identity.Name.Equals("Admin") &&
              !User.Identity.Name.Equals(cdb.Comments.Find(id)))
                RedirectToAction("Block", "Home");

            return View(comment);
        }

        //POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Comment comment = cdb.Comments.Find(id);
            if (comment == null)
                return HttpNotFound();


            if (comment.Hitchhiker == true)
            {
                hdb.Hitchhikers.RemoveRange(hdb.Hitchhikers.Where(c => c.Name == comment.Author));
                hdb.SaveChanges();
            }
            cdb.Comments.Remove(comment);
            cdb.SaveChanges();


            //bugged to fix!
            //var p = pdb.Posts.Include(p => p.Comments.ToList().RemoveAll(cdb => cdb.Data = id));
            Post post = pdb.Posts.Where(p => p.Comments.Any(c => c.Data == id)).SingleOrDefault();
            if (post != null)
            {
                post.Comments.Remove(post.Comments.Where(p => p.Data == id).Single());
                pdb.Entry(post).State = EntityState.Modified;
                pdb.SaveChanges();

            }

            return RedirectToAction("Index", "Users");
            
        }

        

        // GET: Comment/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Comment post = cdb.Comments.Find(id);

            if (post == null)
                return HttpNotFound();

            if (!User.Identity.Name.Equals("Admin") &&
                !User.Identity.Name.Equals(cdb.Comments.Find(id)))
                RedirectToAction("Block", "Home");

            return View(post);
        }

        // POST: Comment/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                Hitchhiker h1 = hdb.Hitchhikers.Where(h => h.Name == comment.Author).SingleOrDefault();
                
                if (comment.Hitchhiker == false)
                {
                    if (h1 != null)
                        hdb.Hitchhikers.RemoveRange(hdb.Hitchhikers.Where(c => c.PostID == h1.PostID));
                       
                }
                else
                {
                    if (h1 != null)
                    {
                        h1.Phone = comment.PhoneNumber;
                        hdb.Entry(h1).State = EntityState.Modified;
                       
                    }
                    else
                    {
                        h1 = new Hitchhiker()
                        { Name = comment.Author, Phone = comment.PhoneNumber, PostID = comment.RefferedPost};
                        hdb.Hitchhikers.Add(h1);
                       
                    }
                }
                hdb.SaveChanges();
                cdb.Entry(comment).State = EntityState.Modified;
                cdb.SaveChanges();
                return RedirectToAction("Index", "Users");
            }
            return RedirectToAction("Index", "Users");
            
        }


      
    }

}
