namespace Recipes.Api.Services.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveAsync(IFormFile file);
    Task DeleteAsync(string? fileUrl);
}