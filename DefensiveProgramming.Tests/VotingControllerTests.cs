using DefensiveProgramming.Controllers;

namespace DefensiveProgramming.Tests
{
    public class VotingControllerTests
    {
        private readonly VotingController _controller;

        public VotingControllerTests()
        {
            _controller = new VotingController(); // Initialize the controller
        }

        [Fact]
        public void CanVote_WhenAgeIsNegative_ShouldThrowArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _controller.CanVote(-1));
        }

        [Fact]
        public void CanVote_WhenAgeIsLessThan18_ShouldReturnFalse()
        {
            // Act
            bool result = _controller.CanVote(16);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanVote_WhenAgeIs18OrMore_ShouldReturnTrue()
        {
            // Act
            bool result = _controller.CanVote(18);

            // Assert
            Assert.True(result);
        }
    }

}
