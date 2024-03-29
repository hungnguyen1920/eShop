﻿using eShop.Data.Entities;
using eShop.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(x => x.Id)
                   .UseIdentityColumn();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status)
                   .HasDefaultValue(Status.Active);
        }
    }
}
