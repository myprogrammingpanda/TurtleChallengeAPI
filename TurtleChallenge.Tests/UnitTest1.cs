using System;
using Microsoft.AspNetCore.Mvc;
using TurtleChallenge.Controllers;
using TurtleChallenge.Models;
using Xunit;

namespace TurtleChallenge.Tests.Controllers
{
    public class TurtleControllerTests
    {
        private readonly TurtleController _controller;

        public TurtleControllerTests()
        {
            _controller = new TurtleController();
        }

        [Fact]
        public void Place_ValidPlacement_ReturnsOk()
        {
            // Act
            var result = _controller.Place(0, 0, Direction.NORTH);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Place_InvalidPlacement_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Place(-1, 0, Direction.NORTH);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Move_TurtleNotPlaced_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Move();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Move_ValidMove_ReturnsOk()
        {
            // Arrange
            _controller.Place(0, 0, Direction.NORTH);

            // Act
            var result = _controller.Move();

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Move_InvalidMove_ReturnsBadRequest()
        {
            // Arrange
            _controller.Place(0, 0, Direction.SOUTH);

            // Act
            var result = _controller.Move();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Left_TurtleNotPlaced_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Left();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Right_TurtleNotPlaced_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Right();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Report_TurtleNotPlaced_ReturnsBadRequest()
        {
            _controller.Remove();
            // Act
            var result = _controller.Report();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Report_TurtlePlaced_ReturnsCorrectPosition()
        {
            // Arrange
            _controller.Place(1, 1, Direction.EAST);

            // Act
            var result = _controller.Report() as OkObjectResult;
            var report = result?.Value as dynamic;

            // Assert
            Assert.NotNull(report);
            Assert.Equal(1, report.X);
            Assert.Equal(1, report.Y);
            Assert.Equal("EAST", report.Facing);
        }

        [Fact]
        public void Remove_TurtleNotPlaced_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Remove();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Remove_TurtlePlaced_ReturnsOk()
        {
            // Arrange
            _controller.Place(1, 1, Direction.NORTH);

            // Act
            var result = _controller.Remove();

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var reportResult = _controller.Report();
            Assert.IsType<BadRequestObjectResult>(reportResult);
        }
    }
}