using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel.ModelConfig
{
    public class StudentAddressConfig : IEntityTypeConfiguration<StudentAddress>
    {
        
        public void Configure(EntityTypeBuilder<StudentAddress> builder)
        {
            builder.ToTable("student_address");
            builder.Property(x => x.Id).HasColumnName("id");
builder.Property(x => x.Address1).HasColumnName("address1").HasMaxLength(30);
builder.Property(x => x.Zipcode).HasColumnName("zipcode");
builder.Property(x => x.State).HasColumnName("state").HasMaxLength(30);
builder.Property(x => x.Country).HasColumnName("country").HasMaxLength(30);
builder.Property(x => x.StudentId).HasColumnName("student_id");
builder.Property(x => x.CreateTime).HasColumnName("create_time");
builder.Property(x => x.CreaterId).HasColumnName("creater_id");
builder.Property(x => x.UpdateTime).HasColumnName("update_time").IsRowVersion();
builder.Property(x => x.UpdaterId).HasColumnName("updater_id");

             
        }
    }
}
