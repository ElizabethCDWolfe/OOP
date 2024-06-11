using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skiing;

namespace SkiingTests
{
    [TestClass]
    public class EnglishTests
    {
        [TestMethod]
        public void SayHello_ShouldOutputHelloToConsole()
        {
            // Arrange
            var english = new English();

            // Redirect console output to a StringWriter
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                english.SayHello();

                // Assert
                string expectedOutput = "Hello" + Environment.NewLine;
                Assert.AreEqual(expectedOutput, sw.ToString());
            }
        }
    }
}
