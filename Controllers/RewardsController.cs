using MicroservicesWebAPI.Constants;
using MicroservicesWebAPI.Data;
using MicroservicesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MicroservicesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {

        private readonly MicroservicesWebAPIContext _context;

        public RewardsController(MicroservicesWebAPIContext context)
        {
            _context = context;
        }

        // GET api/<RewardsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rewards>> Get(int id)
        {
            var transactions = await _context.Transaction.ToListAsync();
            DateTime lastMonthTimestamp = getDateBasedOnOffSetDays(Constant.DAYS_IN_MONTH);
            DateTime lastSecondMonthTimestamp = getDateBasedOnOffSetDays(2 * Constant.DAYS_IN_MONTH);
            DateTime lastThirdMonthTimestamp = getDateBasedOnOffSetDays(3 * Constant.DAYS_IN_MONTH);


            var lastMonthRewardsPoints = transactions.Where(t => t.TransactionDate <= lastMonthTimestamp 
            && t.CustomerId == id).ToList();

            var lastSecondMonthRewardsPoints = transactions.Where(t => t.TransactionDate > lastMonthTimestamp
            && t.TransactionDate >= lastSecondMonthTimestamp && t.CustomerId == id).ToList();

            var lastThirdMonthRewardsPoints = transactions.Where(t => t.TransactionDate > lastSecondMonthTimestamp
            && t.TransactionDate <= lastThirdMonthTimestamp && t.CustomerId == id).ToList();

            var totalRewards = transactions.Where(t => t.CustomerId == id).ToList();


            int lastMonthRewardPoints = getRewards(lastMonthRewardsPoints);
            int lastSecondMonthRewardPoints = getRewards(lastSecondMonthRewardsPoints);
            int lastThirdMonthRewardPoints = getRewards(lastThirdMonthRewardsPoints);
            int totalrewards = getRewards(totalRewards);
            return new Rewards() { CustomerId = id, 
                LastMonthRewardsPoints = lastMonthRewardPoints,                 
                LastSecondMonthRewardPoints = lastSecondMonthRewardPoints,
                LastThridMonthRewardPoints = lastThirdMonthRewardPoints,
                TotalRewards = totalrewards
            };
        }

        private int getRewards(List<Transaction> transactions)
        {
            if (transactions == null)
                return 0;
            var totalAmount = transactions.Sum(item => item.Amount);
            return CalculatePoints(totalAmount);
        }

        private DateTime getDateBasedOnOffSetDays(int days)
        {
            return DateTime.Today.AddDays(-days);
        }

        private static int CalculatePoints(double? totalAmount)
        {
            if (totalAmount != null && totalAmount >= 50)
                return Convert.ToInt32(((totalAmount - Constant.FIRST_REWARD_LIMIT) * 1) + ((totalAmount - Constant.SECOND_REWARD_LIMIT) * 1));
            return 0;

        }
    }
}
