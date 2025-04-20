using FoodApp.Domain.Data.Entities;
using FoodApp.InfraStructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Configurations
{
    public class FavoriteConfiguration : BaseEntityConfiguration<Favorite>
    {
        public override void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.User)
                   .WithMany(u => u.Favorites)
                   .HasForeignKey(f => f.UserId);

            builder.HasOne(f => f.Recipe)
                   .WithMany(u => u.Users)
                   .HasForeignKey(f => f.RecipeId);
        }
    }
}
