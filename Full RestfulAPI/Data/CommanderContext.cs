using Full_RestfulAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Full_RestfulAPI.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> options):base(options)
        {

        }
        public DbSet<Command> Commands { get; set; }
        // we want to represente our Command object as a DbSet
    }
}
