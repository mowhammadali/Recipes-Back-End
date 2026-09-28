namespace Recipes.Api.Models.DTOs.Users;

public sealed record UpdateUserRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
};