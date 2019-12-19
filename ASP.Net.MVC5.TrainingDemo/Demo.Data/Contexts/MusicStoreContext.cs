﻿using Demo.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Contexts
{
    public interface IMusicStoreContext
    {
        DbSet<Album> Album { get; set; }
        DbSet<Artist> Artist { get; set; }
        DbSet<Genre> Genre { get; set; }
    }
    public class MusicStoreContext:DbContext,IMusicStoreContext
    {
        public MusicStoreContext() : base("name=TestLogin")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MusicStoreContext>());
        }
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
    }
}