using AnimalPartyGallery.Context;
using AnimalPartyGallery.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalPartyGallery.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private PostsContext pdb = new PostsContext();
        private CommentsContext cdb = new CommentsContext();
        // GET: Users
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                PostCommentModel pc = new PostCommentModel();
                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                   
                    pc.commentList = cdb.Comments.ToList();
                    pc.postList = pdb.Posts.ToList();
                   
                }
                else
                {
                    pc.commentList = cdb.Comments.Where(c => c.Author==User.Identity.Name).ToList();
                    pc.postList = pdb.Posts.Where(c => c.Producer == User.Identity.Name).ToList();
                    ViewBag.displayMenu = "No";
                }
                return View(pc);

            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }

        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


    }


   
}