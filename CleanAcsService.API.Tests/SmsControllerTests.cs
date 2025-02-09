using System.Threading.Tasks;
using Xunit;
using Moq;
using CleanAcsService.API.Controllers;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Application.DTOs;
using CleanAcsService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanAcsService.API.Tests
{
    public class SmsControllerTests
    {
        [Fact]
        public async Task SendSms_ReturnsOkResult_WhenSmsIsSent()
        {
            // Arrange
            var smsServiceMock = new Mock<ISmsService>();
            smsServiceMock.Setup(x => x.SendSmsAsync(It.IsAny<SmsMessage>()))
                          .Returns(Task.CompletedTask);
            var controller = new SmsController(smsServiceMock.Object);
            var request = new SendSmsRequest
            {
                Destination = "+1234567890",
                Content = "Test message"
            };

            // Act
            var result = await controller.SendSms(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("SMS sent successfully.", okResult.Value);
        }
    }
}
