using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CleanAcsService.API.Controllers;
using CleanAcsService.Application.DTOs;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.API.Tests
{
    public class EmailControllerTests
    {
        [Fact]
        public async Task SendEmail_ReturnsOk_WhenEmailIsSent()
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(s => s.SendEmailAsync(It.IsAny<EmailNotification>()))
                .Returns(Task.CompletedTask);

            var controller = new EmailController(emailServiceMock.Object);

            var request = new SendEmailRequest
            {
                To = "test@example.com",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            // Act
            var result = await controller.SendEmail(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Email sent successfully.", okResult.Value);
        }

        [Fact]
        public async Task SendEmail_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            var controller = new EmailController(emailServiceMock.Object);

            // Act
            var result = await controller.SendEmail(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SendEmail_ReturnsServerError_WhenServiceThrowsException()
        {
            // Arrange
            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(s => s.SendEmailAsync(It.IsAny<EmailNotification>()))
                .ThrowsAsync(new System.Exception("Email error"));

            var controller = new EmailController(emailServiceMock.Object);
            var request = new SendEmailRequest
            {
                To = "test@example.com",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            // Act
            var result = await controller.SendEmail(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
