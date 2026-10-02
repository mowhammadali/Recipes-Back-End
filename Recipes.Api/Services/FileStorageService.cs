using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;

    public FileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveAsync(IFormFile file)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "recipes");

        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(fileStream);

        return $"/images/recipes/{fileName}";
    }

    public Task DeleteAsync(string? fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
            return Task.CompletedTask;

        var fileName = Path.GetFileName(fileUrl);

        var filePath = Path.Combine(
            _environment.WebRootPath,
            "images",
            "recipes",
            fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}