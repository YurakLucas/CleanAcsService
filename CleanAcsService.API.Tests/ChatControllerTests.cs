using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CleanAcsService.API.Controllers;
using CleanAcsService.Application.DTOs;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.API.Tests
{
    public class ChatControllerTests
    {
        [Fact]
        public async Task SendChatMessage_ReturnsOk_WhenMessageIsSent()
        {
            // Arrange
            var chatServiceMock = new Mock<IChatService>();
            chatServiceMock
                .Setup(s => s.SendChatMessageAsync(It.IsAny<ChatMessage>()))
                .Returns(Task.CompletedTask);

            var controller = new ChatController(chatServiceMock.Object);

            var request = new SendChatMessageRequest
            {
                ThreadId = "thread123",
                Sender = "Tester",
                Content = "Hello, chat!"
            };

            // Act
            var result = await controller.SendChatMessage(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Chat message sent successfully.", okResult.Value);
        }

        [Fact]
        public async Task SendChatMessage_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Arrange
            var chatServiceMock = new Mock<IChatService>();
            var controller = new ChatController(chatServiceMock.Object);

            // Act
            var result = await controller.SendChatMessage(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task SendChatMessage_ReturnsServerError_WhenServiceThrowsException()
        {
            // Arrange
            var chatServiceMock = new Mock<IChatService>();
            chatServiceMock
                .Setup(s => s.SendChatMessageAsync(It.IsAny<ChatMessage>()))
                .ThrowsAsync(new System.Exception("Chat error"));

            var controller = new ChatController(chatServiceMock.Object);
            var request = new SendChatMessageRequest
            {
                ThreadId = "thread123",
                Sender = "Tester",
                Content = "Hello, chat!"
            };

            // Act
            var result = await controller.SendChatMessage(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
