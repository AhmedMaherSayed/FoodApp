using FoodApp.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Configurations
{
    public class FoodItemConfiguration : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(f => f.Price)
                   .IsRequired();
            builder.HasOne(f => f.Category)
                   .WithMany(c => c.FoodItems)
                   .HasForeignKey(f => f.CategoryId);
        }
    }
}
