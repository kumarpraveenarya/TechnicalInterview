using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TechnicalInterview.Model.Delivery
{
    [ExcludeFromCodeCoverage]
    public class Order : IValidatableObject
    {
        [Key]
        public Guid OrderNumber { get; set; }

        [Required]
        public string Sender { get; set; }
       

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Sender.Length <=0)
            {
                yield return new ValidationResult($"{Sender} is not a valid");
            }
        }
    }
}