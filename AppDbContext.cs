﻿using CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRUD
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }

}
