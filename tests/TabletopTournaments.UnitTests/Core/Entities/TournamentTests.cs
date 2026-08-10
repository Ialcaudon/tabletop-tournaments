using System;
using FluentAssertions;
using TabletopTournaments.Core.Entities;
using TabletopTournaments.Core.Enums;
using Xunit;

namespace TabletopTournaments.UnitTests.Core.Entities
{
    public class TournamentTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenDataIsValid()
        {
            // Arrange
            string name = "Warhammer Championship";
            DateOnly date = DateOnly.FromDateTime(DateTime.Today).AddDays(10);
            GameSystem gameSystem = GameSystem.WarhammerAoS;

            // Act
            var tournament = new Tournament(name, date, gameSystem);

            // Assert
            tournament.Name.Should().Be(name);
            tournament.Date.Should().Be(date);
            tournament.GameSystem.Should().Be(gameSystem);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsInvalid(string invalidName)
        {
            // Arrange
            DateOnly date = DateOnly.FromDateTime(DateTime.Today).AddDays(10);
            GameSystem gameSystem = GameSystem.Generic;

            // Act
            Action act = () => new Tournament(invalidName, date, gameSystem);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Tournament name cannot be empty (Parameter 'name')");
        }
    }
}
