using System;
using Microsoft.EntityFrameworkCore;
using ThemeApi.Domain.Models;

namespace ThemeApi.Data.Context
{
    public class ThemeDbContext : DbContext
    {
        public ThemeDbContext(DbContextOptions<ThemeDbContext> options) : base(options)
        {
        }

        public DbSet<Theme> Themes { get; set; }
    }
}
