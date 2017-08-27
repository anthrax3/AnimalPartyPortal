using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalPartyGallery.Models
{
    public class PostCommentModel
    {
        public List<Comment> commentList { set; get; }
        public List<Post> postList { set; get; }

    }
}