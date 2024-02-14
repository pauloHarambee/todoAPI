using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoitems.Models;

namespace Todoitems.DbContext
{
    public class TodoContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
