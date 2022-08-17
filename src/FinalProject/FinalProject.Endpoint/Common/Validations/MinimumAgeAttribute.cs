using System.ComponentModel.DataAnnotations;

namespace FinalProject.Endpoint.Common.Validations
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        public MinimumAgeAttribute(int minimumAge) => MinimumAge = minimumAge;
        public int MinimumAge { get; }
        public string GetErrorMessage() => $"حداقل سن ثبت نام {MinimumAge} سال است";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var birthDate = value as DateTime?;
            if (birthDate is not null)
            {
                int age = DateTime.Now.Year - birthDate.Value.Year;
                if (age > MinimumAge)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }
    }
}
