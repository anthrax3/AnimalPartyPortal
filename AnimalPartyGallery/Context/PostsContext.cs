using AnimalPartyGallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AnimalPartyGallery.Context
{
    public class PostsContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}