using Microsoft.EntityFrameworkCore;
using MyPlaylist.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaylist.EF
{
    public class MojContext:DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Pjesma> Pjesma { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
    }
}
