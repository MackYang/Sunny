using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryDemo.DbModel.ModelConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.CategoryName).HasColumnName("category_name").HasMaxLength(30);
            builder.Property(x => x.CreateTime).HasColumnName("create_time");
            builder.Property(x => x.CreaterId).HasColumnName("creater_id");
            builder.Property(x => x.UpdateTime).HasColumnName("update_time").IsRowVersion();
            builder.Property(x => x.UpdaterId).HasColumnName("updater_id");


        }
    }
}
