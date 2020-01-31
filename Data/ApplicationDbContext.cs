using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Studenti.Models;

namespace Studenti.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik,IdentityRole, string>
    {
        public DbSet<Korisnik> ApplicationUsers { get; set; }
        public DbSet<ClassAttend> ClassAttends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Studenti.Models.Korisnik> Korisnik { get; set; }

        public DbSet<Studenti.Models.Kolegiji> Kolegiji { get; set; }
    }
}
