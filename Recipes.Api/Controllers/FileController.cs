using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models.DTOs.Common;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IValidator<UploadImageRequest> _uploadImageValidator;

    public FileController(IFileStorageService fileStorageService, IValidator<UploadImageRequest> uploadImageValidator)
    {
        _fileStorageService = fileStorageService;
        _uploadImageValidator = uploadImageValidator;
    }

    [HttpPost("images")]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<string>> UploadImageAsync(
        [FromForm] UploadImageRequest request)
    {
        var validationResult = await _uploadImageValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var imageUrl = await _fileStorageService.SaveAsync(request.File);

        return Ok(imageUrl);
    }

    [HttpDelete("images")]
    public async Task<IActionResult> DeleteImageAsync([FromQuery] string imageUrl)
    {
        await _fileStorageService.DeleteAsync(imageUrl);

        return NoContent();
    }
}