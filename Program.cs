using System.Diagnostics;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
//________________________________
// Very basic solution to CA1
// Author: Therese Hume
//________________________________
namespace CA1_Solution
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
// Note the definition of a menu as an array of string
            int choice = 0;
            string[] mainMenu = { "Ballin Buses:", "Buy Ticket","Check in Return ticket","Print Journey Report", "Exit"};

// These are declared as local variables here - they would be input most likely.
            decimal basePrice = 10m;
            int seatsOnBus = 10;


            decimal totalDue = 0;
            int seatsOccupied = 0;
            decimal moneyTaken = 0;
            int numberOfTickets = 0;


            TestMethods(); // test the two methods

            do
            {
                choice = GetMenuOption(mainMenu);
                switch (choice)
                {
                    case 1:
                        if (seatsOccupied < seatsOnBus)
                        {
                            moneyTaken += BuyTicket(basePrice);
                            seatsOccupied++; numberOfTickets++;
                            Console.WriteLine($"Seats Taken: {seatsOccupied}\n");
                        }
                        else
                        {
                            Console.WriteLine($"Bus Full\nSeats Taken: {seatsOccupied}\n");
                        }
                         break;
                        
                    case 2:
                        if (seatsOccupied < seatsOnBus)
                        {
                            Console.WriteLine($"Ticket Checked in.\n");
                            seatsOccupied++;
                            Console.WriteLine($"Seats Taken: {seatsOccupied}\n");
                        }
                        else
                        {
                            Console.WriteLine($"Bus Full\nSeats Taken: {seatsOccupied}\n");
                        }
                        break;
                    case 3: PrintJourneyReport(seatsOccupied, moneyTaken);break;
                        
                    default:  break;
                }
            }
            while (choice != mainMenu.Length - 1);
        }
        static int GetMenuOption(string[] menu)
        {
            int choice = 0;

            while (choice <= 0 || choice >= menu.Length)
            {
                Console.WriteLine($"{menu[0]}");

                for (int i = 1; i < menu.Length; i++)
                {
                    Console.WriteLine($"{i}. {menu[i]}");
                }

                choice = int.Parse(Console.ReadLine());// could check for valid integer here also

                if (choice <= 0 || choice >= menu.Length)
                {
                    Console.WriteLine("Incorrect menu choice");
                }
            }
            return choice;

        }

        static decimal BuyTicket(decimal basePrice)
        {
            decimal price = 0;
            
            
            string[] ticketMenu = { "Please Enter Ticket Type:", "Single", "Return" };
            string[] customerMenu = { "Please Enter Customer Type:", "Adult", "Child", "OAP", "Student" };

            string ticketType = ticketMenu[(GetMenuOption(ticketMenu))];
            string customerType = customerMenu[(GetMenuOption(customerMenu))];

            price = CalculateTicketPrice(basePrice, ticketType);
            price = ApplyDiscount(price, customerType);

            Console.WriteLine($"\nBallin-Sligo: {customerType} {ticketType} :  {price:C}\n");
            return price;
        }
        static decimal CalculateTicketPrice(decimal basePrice, string ticketType)
        {
            if (basePrice < 0m) return -1;

            decimal price = 0m;

            switch (ticketType.ToLower())
            {
                case "single": price = basePrice; break;
                case "return": price = basePrice*1.5m; break;
                default: price = basePrice; break;
            }

            return price;
        }
        static decimal ApplyDiscount(decimal price, string customerType)
        {
            if (price < 0) return -1;

            switch (customerType.ToLower())
            {
                case "student": price = price * (0.7m);break;
                case "child": price = price * (0.5m); break;
                case "oap": price = 0m; break;
                default: break; // price remains unchanged.
            }
            
            return price;
        }
        static void PrintJourneyReport(int seats, decimal moneyTaken)
        {
            Console.WriteLine($"\nJourney Report");
            Console.WriteLine($"______________");
            Console.WriteLine($"Seats Occupied : {seats}");
            Console.WriteLine($"Money Taken : {moneyTaken}\n");
        }




        static void TestMethods()
        {
            Console.WriteLine(CalculateTicketPrice(10, "Single") == 10);
            Console.WriteLine(CalculateTicketPrice(11, "Return") == 16.5m);
            Console.WriteLine(CalculateTicketPrice(-9, "Single") == -1);
            Console.WriteLine(CalculateTicketPrice(10, "") == 10);
            Console.WriteLine(CalculateTicketPrice(12, "return") == 18);
            Console.WriteLine(ApplyDiscount(10, "Adult") == 10);
            Console.WriteLine(ApplyDiscount(11, "Child") == 5.5m);
            Console.WriteLine(ApplyDiscount(12, "Student") == 8.4m);
            Console.WriteLine(ApplyDiscount(10, "OAP") == 0);
            Console.WriteLine(ApplyDiscount(10, "oap") == 0);
            Console.WriteLine(ApplyDiscount(10, "travelcard") == 10);
            Console.WriteLine(ApplyDiscount(-11, "OAP") == -1);
            Console.WriteLine(ApplyDiscount(10, "") == 10);



        }
    }
}

