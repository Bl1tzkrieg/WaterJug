using System.ComponentModel.DataAnnotations;

namespace WaterJug.Models.Request
{
    public class WaterJugDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Jug1 capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Jug1 capacity must be a positive non-zero integer.")]
        public int Jug1Capacity { get; set; }
        [Required(ErrorMessage = "Jug2 capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Jug2 capacity must be a positive non-zero integer.")]
        public int Jug2Capacity { get; set; }
        [Required(ErrorMessage = "Target amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Target amount must be a positive non-zero integer.")]
        public int Target { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Jug1Capacity < Target && Jug2Capacity < Target)
            {
                yield return new ValidationResult(
                    "Invalid input: Both bottles are smaller than the target amount."
                );
            }
        }
    }
}
