using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkToTgRetranslator.Models;

namespace VkToTgRetranslator
{
	internal class AppDbContext : DbContext
	{
		public DbSet<DbConversation> Conversations { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("filename=VkToTg.db");
		}
    }
}
