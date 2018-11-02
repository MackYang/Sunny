using RepositoryDemo.DbModel;
using Sunny.Common.DependencyInjection;
using System;

namespace ServiceDemo
{
    public interface IStudentServic:IScoped
    {

        Student GetStudent();
    }
}
