namespace HrLeaveManagement.Application.Models.Identity;

public class AuthResponse
{
    public required string Id { get; set; }
    public string? UserName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}