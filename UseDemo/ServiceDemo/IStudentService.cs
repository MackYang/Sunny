using RepositoryDemo.DbModel;
using Sunny.Common.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ServiceDemo
{
    public interface IStudentServic:IScoped
    {

        Task<Student> GetStudent();

        Task<Student> GetStudent2();

        Task<Student> BizExceptionTest();
    }


    public class SomeoneClass : IScoped
    {

        public string SomeoneMethod()
        {
            return "hello this is di class";
        }
    }
}
