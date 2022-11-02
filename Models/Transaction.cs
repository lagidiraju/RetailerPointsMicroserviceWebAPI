namespace MicroservicesWebAPI
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
    }
}
