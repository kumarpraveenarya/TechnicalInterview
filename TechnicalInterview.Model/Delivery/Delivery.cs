using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechnicalInterview.Model.Enums;

namespace TechnicalInterview.Model.Delivery
{
    public class Delivery : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; private set; } = DateTime.Now;

        public DateTime Endtime { get; set; }

        public virtual Recepient Recepient { get; set; }      

        public virtual Order Order { get; set; }

        private DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Created;

        public void SetStatus(DeliveryStatus deliveryStatus)
        {
            DeliveryStatus = deliveryStatus;
        }

        public DeliveryStatus GetStatus()
        {
            return DeliveryStatus;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Endtime <= DateTime.Now)
            {
                yield return new ValidationResult($"Delivery EndTime {Endtime} is not a valid");
            }

            if (Recepient == null)
            {
                yield return new ValidationResult($"Recepient should be in delivery");
            }

            if (Order == null)
            {
                yield return new ValidationResult($"Order should be in delivery");
            }
        }
    }
}
