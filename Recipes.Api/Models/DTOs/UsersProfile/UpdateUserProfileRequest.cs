namespace Recipes.Api.Models.DTOs.UsersProfile;

public sealed record UpdateUserProfileRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
}