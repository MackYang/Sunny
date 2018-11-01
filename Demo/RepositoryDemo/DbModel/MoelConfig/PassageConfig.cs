using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel.ModelConfig
{
    public class PassageConfig : IEntityTypeConfiguration<Passage>
    {
        
        public void Configure(EntityTypeBuilder<Passage> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(30);
builder.Property(x => x.LastEditTime).HasColumnName("last_edit_time");
builder.Property(x => x.CreateTime).HasColumnName("create_time");
builder.Property(x => x.CreaterId).HasColumnName("creater_id");
builder.Property(x => x.UpdateTime).HasColumnName("update_time").IsRowVersion();
builder.Property(x => x.UpdaterId).HasColumnName("updater_id");

             
        }
    }
}
