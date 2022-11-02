using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicesWebAPI
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        [NotMapped]
        public int Points { get; set; }
    }
}
