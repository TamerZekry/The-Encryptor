using System;
using System.Globalization;
using System.Windows.Controls;

namespace The_Encryptor
{
    public class TextValidationRule : ValidationRule
    {
        public int MinCharachters { get; set; }
        public int MaxCharachters { get; set; }



        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string UserInput = value as String;

            if (UserInput.Length < MinCharachters || UserInput.Length > MaxCharachters || UserInput.Length == 0)
            {
                return new ValidationResult(false, $"The entered text must be at least {MinCharachters} and  not more then {MaxCharachters} Characters");
            }
            return new ValidationResult(true, null);
        }

    }
}
