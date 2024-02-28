using System.Text;

//________________________________________________________________________________________________________________
// Updated CA1 -  Very Basic Solution - note where there might be problems e.g. difficulty testing/reusing code.
//________________________________________________________________________________________________________________

namespace CA1_Solution
{
 
    internal class Program
    {
        // Basic information for the journey -- this is not ideal - we will restructure the  code  later using classes.

        static int[] minsToFinalDestination = { 40, 30, 20, 15, 10, 0 }; // travel time to the final destination in minutes.
        static string[] destinations = { "Ballyshannon", "Bundoran", "Cliffoney", "Grange", "Rathcormack", "Sligo" }; // stops on the route.
        static decimal basePrice = 10m;
        static int seatsOnBus = 10;

        // Statistics for the journey - this is not ideal - we will restructure this code later using classes.

        static int seatsOccupied;
        static decimal moneyTaken = 0;

        static int numberOfTickets = 0;
        static int numberOfReturns = 0;

        static int[] numberBoughtAt = new int[destinations.Length];
        static int[] numberBoughtFor = new int[destinations.Length];

        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Note the definition of a menu as an array of string

            int choice = 0;

            string[] mainMenu = {  "Buy Ticket", "Check in Return ticket", "Print Journey Report", "Exit" };

            do
            {
                choice = GetMenuOption("Ballin Buses:", mainMenu);
                switch (choice)
                {
                    case 0: BuyTicket(); break;
                    case 1: CheckInTicket(); break;
                    case 2: PrintJourneyReport(); break;
                    default: break;
                }
            }
            while (choice != mainMenu.Length - 1);
        }

        /// <summary>
        /// This method prints out a menu passes as an array of strings and returns the chosen option.
        /// We could rewrite this to split the header as a seperate string. 
        /// </summary>
        /// <param name="menu">a string array of menu options </param>
        /// <returns> an integer representing the index of the menu item chosen.</returns>
        static int GetMenuOption(string header, string[] menuOptions)
        {
            int choice = 0;
            Console.WriteLine(header);


            while (choice <= 0 || choice > menuOptions.Length)
            {
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine($"{i+1}. {menuOptions[i]}");
                }
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number");
                }


                if (choice <= 0 || choice > menuOptions.Length)
                {
                    Console.WriteLine("Incorrect menu choice");
                }
            }
            return choice-1;

        }
/// <summary>
/// This method checks in a ticket if there are seats on the bus.
/// </summary>
/// <returns></returns>
        static void CheckInTicket()
        {
            if (seatsOccupied < seatsOnBus)
            {
                
                seatsOccupied++;
                numberOfReturns++;

                Console.WriteLine($"Ticket Checked in.\n");
                Console.WriteLine($"Seats Taken: {seatsOccupied}\n");
               
            }
            else
            {
                Console.WriteLine($"Bus Full\nSeats Taken: {seatsOccupied}\n");
            }
            
        }
        /// <summary>
        /// This method buys a ticket - if there are seats on the bus.
        /// </summary>
        static void BuyTicket()
        {
            decimal price = 0;

            if (seatsOccupied < seatsOnBus)
            {
                string[] ticketMenu = { "Single", "Return" };
                string[] customerMenu = { "Adult", "Child", "OAP", "Student" };

// Get the type of ticket required and the type of customer

                string ticketType = ticketMenu[(GetMenuOption("Please Enter Ticket Type:", ticketMenu))];
                string customerType = customerMenu[(GetMenuOption("Please Enter Customer Type:", customerMenu))];

//Find the origin and destination of the journey

                string origin = destinations[GetMenuOption("Please Enter Origin:", destinations)];
                string destination = destinations[GetMenuOption("Please Enter Destination:", destinations)];

                int indexO = Array.IndexOf(destinations, origin);
                int indexD = Array.IndexOf(destinations, destination);

 //calculate the price and apply discounts

                price = CalculateBasicPrice(basePrice, minsToFinalDestination[0], minsToFinalDestination[indexO], minsToFinalDestination[indexD]);
                price = CalculateTicketPrice(price, ticketType);
                price = ApplyDiscount(price, customerType);

  
  //Record journey data
  
                seatsOccupied++;

                moneyTaken += price;
                numberOfTickets++;
                numberBoughtAt[indexO]++;
                numberBoughtFor[indexD]++;
               
// Print out details of the ticket

                Console.WriteLine($"\n {customerType} {ticketType} from {destinations[indexO]} to {destinations[indexD]}:  {price:C}\n");
                Console.WriteLine($"Seats Taken: {seatsOccupied}\n");
            }
            else
            {
                Console.WriteLine($"Seats Taken: {seatsOccupied}    Bus is Full.\n");
            }
            
        }
        /// <summary>
        /// This method finds the basic price for a journey based on the time travelled between 2 stops.Absolute value means that it will work for both directions.
        /// </summary>
        /// <param name="basePrice - full ticket price"></param>
        /// <param name="totalJourneyTime - total journey time from first to last stop in minutes" ></param> 
        /// <param name="timeFromOrigin - journey time from point of embarkation to final stop in minutes"></param>
        /// <param name="timeFromDestination- journey time from destination stop to final stop in minutes"></param>
        /// <returns>a decimal-  basic ticket price for that journey</returns>
        static decimal CalculateBasicPrice(decimal basePrice, int totalJourneyTime, int timeFromOrigin, int timeFromDestination)
        {
            decimal price = 0;

            decimal pricePerMinute = basePrice / totalJourneyTime;
          
            price = Math.Abs((timeFromOrigin - timeFromDestination)) * pricePerMinute;
           
            return price;
        }
        static decimal CalculateTicketPrice(decimal basePrice, string ticketType)
        {
            if (basePrice < 0m) return -1;

            decimal price = 0m;

            switch (ticketType.ToLower())
            {
                case "single": price = basePrice; break;
                case "return": price = basePrice * 1.5m; break;
                default: price = basePrice; break;
            }

            return price;
        }
        static decimal ApplyDiscount(decimal price, string customerType)
        {
            if (price < 0) return -1;

            switch (customerType.ToLower())
            {
                case "student": price = price * (0.7m); break;
                case "child": price = price * (0.5m); break;
                case "oap": price = 0m; break;
                default: break; // Superflous code- for anything else the price remains unchanged.
            }

            return price;
        }
        static void PrintJourneyReport()
        {
            Console.WriteLine($"\nJourney Report");
            Console.WriteLine($"______________");
            Console.WriteLine($"Seats Occupied : {seatsOccupied}");
            Console.WriteLine($"Money Taken : {moneyTaken}\n");
            Console.WriteLine($"Tickets Bought: {numberOfTickets}");
            Console.WriteLine($"Return Tickets: {numberOfReturns}");

            for(int i=0;i<destinations.Length;i++) 
            {
                Console.WriteLine($"Tickets bought at {destinations[i]}   = {numberBoughtAt[i]}");
                Console.WriteLine($"Tickets bought for {destinations[i]}   = {numberBoughtFor[i]}");
            }
        }
    }
}

