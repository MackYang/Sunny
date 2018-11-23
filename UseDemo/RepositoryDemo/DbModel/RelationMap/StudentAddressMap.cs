using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel.RelationMap
{
    public class StudentAddressMap : IEntityTypeConfiguration<StudentAddress>
    {
        public void Configure(EntityTypeBuilder<StudentAddress> builder)
        {
            builder.HasOne(x => x.Student).WithOne(s => s.Address).HasForeignKey<StudentAddress>(x => x.StudentId);
        }
    }
}
