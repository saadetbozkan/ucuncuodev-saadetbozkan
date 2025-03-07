﻿using Data.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class CollectionSystemDbContext :DbContext, ICollectionSystemDbContext
    {
        public CollectionSystemDbContext(DbContextOptions<CollectionSystemDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Container> Container { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
