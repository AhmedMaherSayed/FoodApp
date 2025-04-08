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
    public class MenuFoodItemConfiguration : IEntityTypeConfiguration<MenuFoodItem>
    {
        public void Configure(EntityTypeBuilder<MenuFoodItem> builder)
        {
            builder.HasKey(mf => new { mf.MenuId, mf.FoodItemId });

            builder.HasOne(mf => mf.Menu)
                   .WithMany(m => m.MenuFoodItems)
                   .HasForeignKey(mf => mf.MenuId);

            builder.HasOne(mf => mf.FoodItem)
                   .WithMany(fi => fi.MenuFoodItems)
                   .HasForeignKey(mf => mf.FoodItemId);
        }
    }
}
