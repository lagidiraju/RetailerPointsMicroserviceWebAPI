namespace MicroservicesWebAPI.Models
{
    public class Rewards
    {
        public int CustomerId { get; set; }
        public int LastMonthRewardsPoints { get; set; }
        public int LastSecondMonthRewardPoints { get; set; }
        public int LastThridMonthRewardPoints { get; set; }
        public int TotalRewards { get; set; }
    }
}