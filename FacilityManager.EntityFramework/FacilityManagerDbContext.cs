﻿using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.EntityFramework
{
    public class FacilityManagerDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Facility> Facility { get; set; }

        public FacilityManagerDbContext(DbContextOptions<FacilityManagerDbContext> options)
            : base(options)
        {
        }
    }
}