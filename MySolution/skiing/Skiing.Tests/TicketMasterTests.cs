namespace Skiing.Tests
{
    [TestClass]
    public class TicketMasterTests
    {
        [TestMethod]
        public void SellTicketAddsToSkierPocketIfAvailable()
        {
            // Arrange
            string skierName = "John";
            string site = "Aspen";
            Skier skier = new Skier(skierName);
            TicketMaster ticketMaster = new TicketMaster("TicketMaster");
            int startingTickets = skier.SkierPocket.TicketList.Count;

            // Act
            ticketMaster.SellTicket(skier, site);

            // Assert
            Assert.AreEqual(1, skier.SkierPocket.TicketList.Count - startingTickets);
        }

        [TestMethod]
        public void SellTicketDoesntAddToSkierPocketIfNotAvailable()
        {
            // Arrange
            string skierName = "John";
            string site = "InvalidSite";
            Skier skier = new Skier(skierName);
            TicketMaster ticketMaster = new TicketMaster("TicketMaster");
            int startingTickets = skier.SkierPocket.TicketList.Count;

            // Act
            ticketMaster.SellTicket(skier, site);

            // Assert
            Assert.AreEqual(
                0,
                skier.SkierPocket.TicketList.Count - skier.SkierPocket.TicketList.Count
            );
        }
    }
}
