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
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Recipes)
                   .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Category)
                   .WithMany(c => c.Recipes)
                   .HasForeignKey(r => r.CategoryId);
        }
    }
}
