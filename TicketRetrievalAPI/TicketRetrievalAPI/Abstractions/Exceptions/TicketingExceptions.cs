using System.Globalization;

namespace TicketRetrievalAPI.Abstractions.Exceptions
{
    public class TicketingExceptions : Exception
    {
        public TicketingExceptions() : base() { }

        public TicketingExceptions(string message) : base(message) { }

        public TicketingExceptions(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
