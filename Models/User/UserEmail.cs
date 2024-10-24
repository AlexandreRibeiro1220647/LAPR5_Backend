using System.Text.RegularExpressions;

namespace TodoApi.Models;

public class UserEmail 
{
    private static readonly Regex emailRegex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    public string Value { get; private set; }

    public UserEmail(){}

    public UserEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty");
        }

        if (!emailRegex.IsMatch(email))
        {
            throw new ArgumentException("Email format is invalid", nameof(email));
        }

        Value = email;
    }

    
}