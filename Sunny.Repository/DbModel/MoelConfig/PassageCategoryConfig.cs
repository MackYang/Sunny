using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel.MoelConfig
{
    public class PassageCategoryConfig : IEntityTypeConfiguration<PassageCategory>
    {
        
        public void Configure(EntityTypeBuilder<PassageCategory> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
builder.Property(x => x.CategoryId).HasColumnName("category_id");
builder.Property(x => x.PassageId).HasColumnName("passage_id");
builder.Property(x => x.CreateTime).HasColumnName("create_time");
builder.Property(x => x.CreaterId).HasColumnName("creater_id");
builder.Property(x => x.UpdateTime).HasColumnName("update_time").IsRowVersion();
builder.Property(x => x.UpdaterId).HasColumnName("updater_id");

             
        }
    }
}
