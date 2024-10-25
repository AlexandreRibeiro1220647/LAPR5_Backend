using TodoApi.Models.Shared;

namespace TodoApi.Models.Shared;

public class Phone
{
    public string phoneNumber { get; private set; }

    private readonly string[] allowedFormats = { @"^(9|3519)[1236]\d{7}$" };

    protected Phone()
    {

    }


    public Phone(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            throw new ArgumentNullException(nameof(phoneNumber));
        }

        if (!allowedFormats.Any(format => IsValidFormat(phoneNumber, format)))
        {
            throw new ArgumentException("Phone number must be a valid portuguese phone number");
        }

        this.phoneNumber = phoneNumber;
    }

    private static bool IsValidFormat(string phoneNumber, string format)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, format);
    }
}