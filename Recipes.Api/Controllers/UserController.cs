using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Models.DTOs.Users;
using Recipes.Api.Services.Interfaces;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<AdminUpdateUserRequest> _adminUpdateUserValidator;

    public UserController(IUserService userService, IValidator<AdminUpdateUserRequest> adminUpdateUserValidator)
    {
        _userService = userService;
        _adminUpdateUserValidator = adminUpdateUserValidator;
    }

    [HttpGet("get-all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<UserResponse>>> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("get-by-id/{userId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserResponse>> GetByIdAsync([FromRoute] Guid userId)
    {
        var user = await _userService.GetByIdAsync(userId);

        return Ok(user);
    }

    [HttpDelete("remove/{userId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid userId)
    {
        await _userService.DeleteAsync(userId);
        return NoContent();
    }

    [HttpPut("update-by-admin/{userId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateByAdminAsync([FromRoute] Guid userId,
        [FromBody] AdminUpdateUserRequest adminUpdateUserRequest)
    {
        var validationResult = await _adminUpdateUserValidator.ValidateAsync(adminUpdateUserRequest);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _userService.UpdateByAdminAsync(userId, adminUpdateUserRequest);
        return NoContent();
    }
}