using System;
using System.Linq;
using RepositoryDemo;
using RepositoryDemo.DbModel;

namespace ServiceDemo
{
    public class StudentService : IStudentServic
    {
        MyDbContext db;
        public StudentService(MyDbContext db)
        {
            this.db = db;
        }

        public Student GetStudent()
        {
            return db.Student.FirstOrDefault();
        }
    }
}
