using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel.ModelConfig
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
builder.Property(x => x.StudentName).HasColumnName("student_name").HasMaxLength(30);
builder.Property(x => x.Test).HasColumnName("test").HasMaxLength(30);
builder.Property(x => x.AAA).HasColumnName("a_a_a").HasMaxLength(30);
builder.Property(x => x.Score).HasColumnName("score").HasColumnType("decimal(18, 2)");
builder.Property(x => x.CreateTime).HasColumnName("create_time");
builder.Property(x => x.CreaterId).HasColumnName("creater_id");
builder.Property(x => x.UpdateTime).HasColumnName("update_time").IsRowVersion();
builder.Property(x => x.UpdaterId).HasColumnName("updater_id");

             
        }
    }
}
