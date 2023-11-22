namespace WebshopBackend.Exceptions
{
    public class ProductNotFoundException : Exception
    {
            public ProductNotFoundException() : base("Er ging iets mis met het ophalen van je product") { }
            public ProductNotFoundException(string message) : base(message) { }
            public ProductNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
