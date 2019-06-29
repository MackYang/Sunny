using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel.ModelConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");
            builder.Property(x => x.Id).HasColumnName("id");
builder.Property(x => x.CategoryName).HasColumnName("category_name").HasMaxLength(30);
builder.Property(x => x.CreateTime).HasColumnName("create_time").HasDefaultValue(DateTime.Now);
builder.Property(x => x.CreaterId).HasColumnName("creater_id");
builder.Property(x => x.UpdateTime).HasColumnName("update_time").IsRowVersion();
builder.Property(x => x.UpdaterId).HasColumnName("updater_id");
            

             
        }
    }
}
