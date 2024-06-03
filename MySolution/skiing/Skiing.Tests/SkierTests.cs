using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Skiing.Tests
{
    [TestClass]
    public class SkierTests
    {
        [TestMethod]
        public void SkierConstructorDefaultGreeting()
        {
            // Arrange
            string expectedName = "John";

            // Act
            Skier skier = new Skier(expectedName);

            // Assert
            Assert.AreEqual(expectedName, skier.Name);
            Assert.IsInstanceOfType(skier.Greeting, typeof(English));
            Assert.IsNotNull(skier.SkierPocket);
        }

        [TestMethod]
        public void SkierConstructorCustomGreeting()
        {
            // Arrange
            string expectedName = "Jane";
            IGreeting customGreeting = new Spanish();

            // Act
            Skier skier = new Skier(expectedName, customGreeting);

            // Assert
            Assert.AreEqual(expectedName, skier.Name);
            Assert.AreSame(customGreeting, skier.Greeting);
            Assert.IsNotNull(skier.SkierPocket);
        }
    }
}
