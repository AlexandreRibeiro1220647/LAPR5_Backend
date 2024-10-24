using TodoApi.Models.Shared;

namespace TodoApi.Models.Shared;

using System;
using System.Text.RegularExpressions;

public class Password
{
    private string _value;

    public string Value
    {
        get => _value;
        private set
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Password must be at least 10 characters long, contain at least one digit, one capital letter, and one special character.");
            }
            _value = value;
        }
    }

    public Password(string value)
    {
        Value = value;
    }

    private bool IsValid(string password)
    {
        if (password.Length < 8)
            return false;

        bool hasDigit = false;
        bool hasUpperCase = false;
        bool hasSpecialChar = false;

        foreach (char c in password)
        {
            if (char.IsDigit(c)) hasDigit = true;
            if (char.IsUpper(c)) hasUpperCase = true;
            if (!char.IsLetterOrDigit(c)) hasSpecialChar = true;
        }

        return hasDigit && hasUpperCase && hasSpecialChar;
    }
}