using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SitemapParse.Models
{
    public class URL
    {
        public int ID { get; set; }
        public string url { get; set; }
    }

    public class URLContext : DbContext
    {
        public DbSet<URL> urls { get; set; }
    }
}