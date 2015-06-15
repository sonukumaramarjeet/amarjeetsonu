using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using JQGridMVC.Models;

namespace JQGridMVC.DBContext
{
    public class TodoContext:DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
    }
}