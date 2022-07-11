using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TechnicalInterview.Model.Delivery
{
    [ExcludeFromCodeCoverage]
    public class Recepient : IValidatableObject
    {   
        [Key]
        public int Id { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }
                
        [MaxLength(64)]
        public string LastName { get; set; }       

        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
                
        [Required]
        [StringLength(10, ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }
                
        public string Address { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {  
            if (!new EmailAddressAttribute().IsValid(EmailAddress))
            {
                yield return new ValidationResult($"{EmailAddress} is not a valid Email");
            }

            if (PhoneNumber.Length != 10)
            {
                yield return new ValidationResult($"{PhoneNumber} is not a valid Phone");
            }
        }
    }
}
