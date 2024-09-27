using Orders.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders.Attributes
{
    public class OrderSideValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string side)
            {
                var validSides = Enum.GetNames(typeof(OrderSide));

                if (validSides.Contains(side))
                {
                    return ValidationResult.Success;
                }

                string validValues = string.Join(", ", validSides);
                return new ValidationResult($"The Side must be one of the following values: {validValues}.");
            }

            return new ValidationResult("Invalid data type for Side.");
        }
    }

}
