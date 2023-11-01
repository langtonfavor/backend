using System;
using Xunit;
using backend.Controllers;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using backend.Models;
using backend.Services;

public class UserControllerTests
{
    [Fact]
public void Register_ValidUser_ReturnsOkResult()
{
    // Arrange
    var userServiceMock = new Mock<IUserService>();
    userServiceMock.Setup(service => service.Register(It.IsAny<User>())).Returns(new User());

    var controller = new UserController(userServiceMock.Object, null, null);

    // Act
    var result = controller.Register(new User()) as OkResult;

    // Assert
    Assert.NotNull(result);
    Assert.Equal(200, result.StatusCode);
}

}