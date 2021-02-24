using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AuthJWT
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builer)
        {
            builer.ToTable("users");
            builer.HasKey(b => b.Id);
            builer.Property(b => b.Id)
                .ValueGeneratedOnAdd();
            builer.Property(x => x.UserName).HasMaxLength(50);
            builer.Property(x => x.Password).HasMaxLength(256);
            builer.Property(x => x.IsAdmin).HasDefaultValue(false);
        }
    }
}
