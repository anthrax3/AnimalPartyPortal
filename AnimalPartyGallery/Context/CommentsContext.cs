using AnimalPartyGallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AnimalPartyGallery.Context
{
    public class CommentsContext:DbContext
    {
        public DbSet<Comment> Comments { get; set; }

    }
}