using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Student> GetStudent()
        {
            return await Task.Run(()=>db.Student.FirstOrDefault());
        }
    }
}
