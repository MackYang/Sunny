using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel.MoelConfig
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        /// <summary>
        /// PassageCategories FluentAPI配置
        /// 
        /// 添加复合主键、配置多对多关系
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.StudentName).HasMaxLength(15);
            builder.Property(x => x.Score).HasColumnName("sss").IsRowVersion();
             
        }
    }
}
