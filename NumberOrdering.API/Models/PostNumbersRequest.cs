using System.ComponentModel.DataAnnotations;

namespace NumberOrdering.API.Models
{
    public class PostNumbersRequest
    {
        [Required]
        [MinLength(1)]
        public int[] Numbers { get; set; }
    }
}
