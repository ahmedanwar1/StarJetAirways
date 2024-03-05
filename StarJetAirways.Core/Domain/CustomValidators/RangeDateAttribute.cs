using System.ComponentModel.DataAnnotations;

namespace StarJetAirways.Core.Domain.CustomValidators
{
    public class RangeDateAttribute : ValidationAttribute
    {
        private DateTime _minDate = DateTime.MinValue;
        private DateTime _maxDate = DateTime.MaxValue;

        //to enable comparing to today's date
        public bool ValidUntilCurrentDate { get; set; } = false;

        public RangeDateAttribute(string minDate)
        {
            if (!DateTime.TryParse(minDate, out _minDate))
            {
                throw new ArgumentException("Invalid min date format or value", nameof(minDate));
            }

        }

        public RangeDateAttribute(string minDate, string maxDate)
        {
            if (!DateTime.TryParse(minDate, out _minDate))
            {
                throw new ArgumentException("Invalid min date format or value", nameof(minDate));
            }

            if (!string.IsNullOrEmpty(maxDate) && !DateTime.TryParse(maxDate, out _maxDate))
            {
                throw new ArgumentException("Invalid max date format or value", nameof(maxDate));
            }

            if (_minDate > _maxDate)
            {
                throw new ArgumentException("min date must be before max date");
            }

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                //compare min date to today's date
                if (ValidUntilCurrentDate == true)
                {
                    if (_minDate <= DateTime.Now)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage ?? string.Format("Min Date must be less than current date"));
                    }
                }

                //compare min date to max date
                if (date >= _minDate && date <= _maxDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? string.Format("Date must be between {0} and {1}", _minDate, _maxDate));
                }
            }

            return new ValidationResult(string.Format("Invalid data type for this attribute."));
        }
    }
}
