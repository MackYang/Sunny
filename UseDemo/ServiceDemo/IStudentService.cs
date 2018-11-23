using RepositoryDemo.DbModel;
using Sunny.Common.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ServiceDemo
{
    public interface IStudentServic:IScoped
    {

        Task<Student> GetStudent();
    }
}
