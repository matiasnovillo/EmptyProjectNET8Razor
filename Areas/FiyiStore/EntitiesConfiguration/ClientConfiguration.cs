using FiyiStore.Areas.FiyiStore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStore.Areas.FiyiStore.EntitiesConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            try
            {
                //ClientId
                entity.HasKey(e => e.ClientId);
                entity.Property(e => e.ClientId)
                    .ValueGeneratedOnAdd();

                //Active
                entity.Property(e => e.Active)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //DateTimeCreation
                entity.Property(e => e.DateTimeCreation)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //DateTimeLastModification
                entity.Property(e => e.DateTimeLastModification)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //UserCreationId
                entity.Property(e => e.UserCreationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //UserLastModificationId
                entity.Property(e => e.UserLastModificationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Name
                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .IsRequired(true);

                //Age
                entity.Property(e => e.Age)
                    .HasColumnType("int")
                    .IsRequired(true);

                //EsCasado
                entity.Property(e => e.EsCasado)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //BornDateTime
                entity.Property(e => e.BornDateTime)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //Height
                entity.Property(e => e.Height)
                    .HasColumnType("numeric(18, 2)")
                    .IsRequired(true);

                //Email
                entity.Property(e => e.Email)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(true);

                //ProfilePicture
                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("varchar(MAX)")
                    .IsRequired(true);

                //FavouriteColour
                entity.Property(e => e.FavouriteColour)
                    .HasColumnType("varchar(7)")
                    .IsRequired(true);

                //Password
                entity.Property(e => e.Password)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(true);

                //PhoneNumber
                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(true);

                //Tags
                entity.Property(e => e.Tags)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(true);

                //About
                entity.Property(e => e.About)
                    .HasColumnType("text")
                    .IsRequired(true);

                //AboutInTextEditor
                entity.Property(e => e.AboutInTextEditor)
                    .HasColumnType("text")
                    .IsRequired(true);

                //WebPage
                entity.Property(e => e.WebPage)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(true);

                //BornTime
                entity.Property(e => e.BornTime)
                    .HasColumnType("time(7)")
                    .IsRequired(true);

                //Colour
                entity.Property(e => e.Colour)
                    .HasColumnType("varchar(7)")
                    .IsRequired(true);

                
            }
            catch (Exception) { throw; }
        }
    }
}
