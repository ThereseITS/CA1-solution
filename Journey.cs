using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA1_Solution
{
    internal class Journey
    {
       
            int seatsOccupied = 0;
            decimal moneyTaken = 0;
            int numberOfTickets = 0;
            int numberOfReturns = 0;

            int[] numberBoughtAt;
            int[] numberBoughtFor;

            int[] minsToFinalDestination;
            string[] destinations;

            decimal basePrice = 10m;
            int seatsOnBus = 10;
            bool reverse = false;

            public Journey(string[] destinations, int[] minsToFinalDestination, bool reverse, decimal basePrice, int seatsOnBus)
            {
                numberBoughtAt = new int[destinations.Length];
                numberBoughtFor = new int[destinations.Length];
                this.destinations = destinations;
                this.minsToFinalDestination = minsToFinalDestination;
                this.basePrice = basePrice;
                this.seatsOnBus = seatsOnBus;
                this.reverse = reverse;
            }


        public decimal BuyTicket(string origin, string destination, string ticketType, string customerType)
        {

            if(seatsOccupied<seatsOnBus)
            {
                int indexO = Array.IndexOf(destinations, origin);
                int indexD = Array.IndexOf(destinations, destination);

                if (indexO >= 0 && indexD >= 0)
                {

                    decimal price = CalculateBasicPrice(basePrice, minsToFinalDestination[0], minsToFinalDestination[indexO], minsToFinalDestination[indexD]);
                    price = CalculateTicketPrice(price, ticketType);
                    price = ApplyDiscount(price, customerType);

                    numberOfTickets++;
                    seatsOccupied++;
                    moneyTaken += price;
                    numberBoughtAt[indexO]++;
                    numberBoughtFor[indexD]++;

                    return price;
                }
            }
            
                return -1;
        
        }

        public bool CheckInTicket()
        {
            if (seatsOccupied < seatsOnBus)
            {
                seatsOccupied++;
                return true;

            }
            else
            {
                return false;
            }

        }
        public decimal CalculateBasicPrice(decimal basePrice, int totalJourneyTime,int timeFromOrigin, int timeFromDestination)
        {
            decimal price = 0;

            decimal pricePerMinute = basePrice / totalJourneyTime;

            price = Math.Abs((timeFromOrigin - timeFromDestination)) * pricePerMinute;

            return price;
        }
        public decimal CalculateTicketPrice(decimal price, string ticketType)
        {
            if (price < 0m) return -1;
     

            switch (ticketType.ToLower())
            {
                case "single":  break;
                case "return": price = price * 1.5m; break;
                default:  break;
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
        public  void PrintJourneyReport()
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
