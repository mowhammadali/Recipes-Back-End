namespace Recipes.Api.Models.DTOs.Recipes;

public record InstructionRequest(
    int Step,
    string Description);