using Microsoft.EntityFrameworkCore;
using RepositoryDemo.DbModel;

namespace RepositoryDemo
{

    public partial class MyDbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Passage> Passage { get; set; }
        public DbSet<PassageCategory> PassageCategory { get; set; }


        public DbSet<Student> Student { get; set; }

        public DbSet<StudentAddress> StudentAddress { get; set; }

        public DbSet<IdTest> IdTest { get; set; }
    }
}
