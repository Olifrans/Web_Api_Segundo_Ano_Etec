using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core;

namespace WebApiOlifrans.Data
{
    public class WebApiOlifransContext : DbContext
    {
        public WebApiOlifransContext (DbContextOptions<WebApiOlifransContext> options)
            : base(options)
        {
        }

        public DbSet<Core.Pessoa> Pessoa { get; set; }
    }
}
