# OOP
Skiing OOP

Includes: 

-An IGreeting interface which English and Spanish Inherit from. 

-An abstract Person class which Skier inherits from. Ticketmaster inherits 
from Skier. All three classes implement IGreeting. Ticketmaster has
an Inventory and Pocket, and Skier has a Pocket.

-An abstract TicketContainer class which Inventory and Pocket classes
inherit from. Inventory is populated with tickets in its constructor. Pocket
is not. Inventory can remove tickets from itself and return them. Pocket
can add tickets to itself. 

-A Ticket class

To use, run in /OOP/MySolution/skiing/src/

dotnet run

To test, run in /OOP/MySolution/skiing/tests/

dotnet test

--Unit Tests are currently under construction for this project--

