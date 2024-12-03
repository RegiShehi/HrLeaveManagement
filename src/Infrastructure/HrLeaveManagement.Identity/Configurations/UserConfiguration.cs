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
        builder.HasData(
            new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEFC8JBTYZEZqBhpgSErs009OJdIm6+1kEl9PBAbELbf1o19LDPMOK8jqEhL+qk/LSA==",
                EmailConfirmed = true,
                ConcurrencyStamp = "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
                SecurityStamp = "12345678-abcd-1234-abcd-1234567890ab"
            },
            new ApplicationUser
            {
                Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEMOmSIfFuNVCoIc2Y3exbVQ04NqDMQVb96jXY93mxxSH/rZj5bNzEEAo/8e1gs02Qg==",
                EmailConfirmed = true,
                ConcurrencyStamp = "abcd1234-ef56-7890-abcd-1234567890ab", // Static GUID
                SecurityStamp = "abcdef12-3456-7890-abcd-1234567890ab" // Static GUID
            }
        );
    }
}