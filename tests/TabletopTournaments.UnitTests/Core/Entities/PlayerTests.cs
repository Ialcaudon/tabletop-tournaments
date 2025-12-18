using System;
using FluentAssertions;
using TabletopTournaments.Core.Entities;
using Xunit;

namespace TabletopTournaments.UnitTests.Core.Entities
{
    public class PlayerTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenDataIsValid()
        {
            // Arrange
            string name = "Ignacio";

            // Act
            var player = new Player(name);

            // Assert
            player.Name.Should().Be(name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsInvalid(string invalidName)
        {
            // Arrange & Act
            Action act = () => new Player(invalidName);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Player name cannot be empty (Parameter 'name')");
        }
    }
}
