using System.Net.Sockets;

namespace MovieTicketHelper2
{
    public enum TicketType
    {
        Standard,
        VIP,
        IMAX
    }

    public struct Seat
    {
        public char Row;
        public int Number;

        public Seat(char row, int number)
        {
            Row = row;
            Number = number;
        }

        public override string ToString()
        {
            return $"{Row}{Number}";
        }
    }
    public class Ticket
    {
        private static int ticketCounter = 0;

        private int ticketId;
        private string movieName;
        private TicketType type;
        private Seat seat;
        private decimal price;

        public Ticket(string movieName, TicketType type, Seat seat, decimal price)
        {
            ticketCounter++;
            ticketId = ticketCounter;
            MovieName = movieName;   
            Type = type;
            Seat = seat;
            Price = price;
        }

        public string MovieName
        {
            get { return movieName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    movieName = value;
            }
        }

        public TicketType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Seat Seat
        {
            get { return seat; }
            set { seat = value; }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                    price = value;
            }
        }

        public decimal PriceAfterTax
        {
            get { return price * 1.14m; }
        }

        public int TicketId
        {
            get { return ticketId; }
        }
        public static int GetTotalTicketsSold()
        {
            return ticketCounter;
        }
    }

    public class Cinema
    {
        private Ticket[] tickets = new Ticket[20];

        public Ticket this[int index]
        {
            get
            {
                if (index >= 0 && index < tickets.Length)
                    return tickets[index];

                return null; 
            }
            set
            {
                if (index >= 0 && index < tickets.Length)
                    tickets[index] = value;
            }
        }

        public Ticket this[string movieName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(movieName))
                    return null;

                foreach (var ticket in tickets)
                {
                    if (ticket != null && ticket.MovieName == movieName)
                        return ticket; 
                }

                return null;
            }
        }

        public bool AddTicket(Ticket t)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] == null)
                {
                    tickets[i] = t;
                    return true;
                }
            }

            return false;
        }
    }

    public static class BookingHelper
    {
        private static int bookingCounter = 0;

        public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
        {
            double total = numberOfTickets * pricePerTicket;

            if (numberOfTickets >= 5)
                return total * 0.90; 

            return total;
        }

        public static string GenerateBookingReference()
        {
            bookingCounter++;
            return $"BK-{bookingCounter}";
        }
    }
}
