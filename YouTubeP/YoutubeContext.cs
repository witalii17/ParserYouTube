using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeP
{
   
        public class YoutubeContext : DbContext
        {
            public DbSet<YoutubeContext> YoutubeContexts { get; set; }

            public YoutubeContext()
            {
                Database.EnsureCreated();
            }



            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-VITALII ;Initial Catalog=DataBase;Integrated Security=True");
            }

        }

    }


