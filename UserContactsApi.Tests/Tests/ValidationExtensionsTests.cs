using FluentValidation.Results;
using UserContactsApi.Validators;

namespace UserContactsApi.Tests.Tests
{
    public class ValidationExtensionsTests
    {
        [Fact]
        public void ToValidationProblemDetails_WithValidationResult_ReturnsValidationProblemDetails()
        {
            // Arrange
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("PropertyName1", "Error message 1"));
            validationResult.Errors.Add(new ValidationFailure("PropertyName2", "Error message 2"));

            // Act
            var result = validationResult.ToValidationProblemDetails();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("One or more validation errors occurred.", result.Title);
            Assert.Equal(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, result.Status);
            Assert.Equal(2, result.Errors.Count);
            Assert.Contains("PropertyName1", result.Errors.Keys);
            Assert.Contains("PropertyName2", result.Errors.Keys);
            Assert.Single(result.Errors["PropertyName1"]);
            Assert.Single(result.Errors["PropertyName2"]);
            Assert.Equal("Error message 1", result.Errors["PropertyName1"][0]);
            Assert.Equal("Error message 2", result.Errors["PropertyName2"][0]);
        }
    }
}
