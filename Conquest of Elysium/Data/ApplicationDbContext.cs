using Conquest_of_Elysium.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Conquest_of_Elysium.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Sheet> Sheets { get; set; }
		public DbSet<SheetObject> SheetObject { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Sheet>().HasOne(s => s.User).WithMany(s => s.Sheets).OnDelete(DeleteBehavior.Cascade);
		}

	}
}