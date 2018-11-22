using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryDemo.DbModel.RelationMap
{
    public class PassageCategoryMap : IEntityTypeConfiguration<PassageCategory>
    {
        /// <summary>
        /// PassageCategories FluentAPI配置
        /// 
        /// 添加复合主键、配置多对多关系
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<PassageCategory> builder)
        {
            //添加复合主键
            builder.HasKey(t => new { t.PassageId, t.CategoryId });

            ///<summary>
            ///
            /// 配置Passage与PassageCategories的一对多关系
            /// 
            /// EFCore中,新增默认级联模式为ClientSetNull
            /// 
            /// 依赖实体的外键会被设置为空，同时删除操作不会作用到依赖的实体上，依赖实体保持不变，同下
            /// 
            /// </summary>

            //配置Passage与PassageCategories的一对多关系
            builder.HasOne(t => t.Passage).WithMany(p => p.PassageCategories).HasForeignKey(t => t.PassageId);

            //配置Category与PassageCategories的一对多关系
            builder.HasOne(t => t.Category).WithMany(p => p.PassageCategories).HasForeignKey(t => t.CategoryId);
        }
    }
}
