using AnimalPartyGallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AnimalPartyGallery.Context
{
    public class HitchhikersContext:DbContext
    {
        public DbSet<Hitchhiker> Hitchhikers { get; set; }

    }
}