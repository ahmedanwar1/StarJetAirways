using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace StarJetAirways.Core.Domain.CustomValidators;

public class AlphabeticAttribute : ValidationAttribute
{
    public readonly string DefaultErrorMessage = "The text you enter must only consist of characters";

    public bool SpacesAllowed { get; set; } = false;


    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue)
        {
            Regex regexAlphabetic;

            if (SpacesAllowed == true)
            {
                regexAlphabetic = new Regex(@"^[A-Za-z ]+$");
            }
            else
            {
                regexAlphabetic = new Regex(@"^[A-Za-z]+$");
            }

            bool isAlphabetic = regexAlphabetic.IsMatch(stringValue);

            if (isAlphabetic == true)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
    }
}
