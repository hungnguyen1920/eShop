using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Data.Configurations
{
    public class ProductCategoryMapConfiguration : IEntityTypeConfiguration<ProductCategoryMap>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryMap> builder)
        {
            builder.HasKey(t => new { t.CategoryId, t.ProductId });
            builder.ToTable("ProductCategoryMaps");
            builder.HasOne(t => t.Product)
                   .WithMany(pc => pc.ProductCategoryMaps)
                   .HasForeignKey(pc => pc.ProductId);
            builder.HasOne(t => t.Category)
                   .WithMany(pc => pc.ProductCategoryMaps)
                   .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
