using HrLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLeaveManagement.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        // Seed admin user
        var adminUser = new ApplicationUser
        {
            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            Email = "admin@localhost.com",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            FirstName = "System",
            LastName = "Admin",
            UserName = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            EmailConfirmed = true
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "P@ssword1");

        // Seed regular user
        var regularUser = new ApplicationUser
        {
            Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
            Email = "user@localhost.com",
            NormalizedEmail = "USER@LOCALHOST.COM",
            FirstName = "System",
            LastName = "User",
            UserName = "user@localhost.com",
            NormalizedUserName = "USER@LOCALHOST.COM",
            EmailConfirmed = true
        };
        regularUser.PasswordHash = hasher.HashPassword(regularUser, "P@ssword1");

        // Add seeded users to the builder
        builder.HasData(adminUser, regularUser);
    }
}