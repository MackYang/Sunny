using Microsoft.EntityFrameworkCore;
using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository
{
    public partial class EfDbContext
    {

        public DbSet<Category> Category { get; set; }
        public DbSet<Passage> Passage { get; set; }
        public DbSet<PassageCategory> PassageCategory { get; set; }


        public DbSet<User> Uesrs { get; set; }
        public DbSet<UserB> Uesr2s { get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<StudentAddress> StudentAddress { get; set; }
    }
}
