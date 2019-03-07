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

        public async Task<Student> GetStudent2()
        {
            return await Task.Run(() => db.Student.FirstOrDefault());
        }

        public async Task<Student> BizExceptionTest()
        {
            throw new BizException("订单不存在,这是一个测试抛出的业务异常");
        }
    }
}
