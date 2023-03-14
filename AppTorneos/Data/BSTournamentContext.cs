﻿using AppTorneos.Models;
using Microsoft.EntityFrameworkCore;

namespace AppTorneos.Data
{
    public class BSTournamentContext: DbContext
    {
        public BSTournamentContext(DbContextOptions<BSTournamentContext> options) 
        : base(options){ }

        public DbSet<User> Usuarios { get; set; }
        
        public DbSet<Equipo> Equipos { get; set; }
    }
}