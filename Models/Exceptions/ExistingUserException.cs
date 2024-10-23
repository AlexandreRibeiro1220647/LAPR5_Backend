public class ExistingUserException : Exception
    {
        public string Details { get; }

        public ExistingUserException(string message) : base(message)
        {
            
        }

        public ExistingUserException(string message, string details) : base(message)
        {
            this.Details = details;
        }
    }