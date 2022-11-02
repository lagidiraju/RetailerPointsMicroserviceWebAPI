# RetailerPointsMicroserviceWebAPI
Problem: A retailer offers a rewards program to its customers awarding points based on each recorded purchase as follows:
 
For every dollar spent over $50 on the transaction, the customer receives one point.
In addition, for every dollar spent over $100, the customer receives another point.
Ex: for a $120 purchase, the customer receives
(120 - 50) x 1 + (120 - 100) x 1 = 90 points

Run Migrations:
  Go to Tools -> Package Manager console -> run below commands
  
  PM> add-migration rewards_v1
  
  PM> update-database -verbose
  
Run application:
![image](https://user-images.githubusercontent.com/55935302/199392583-d89c2c93-822f-4a04-a55d-e6f4c24efc54.png)

Add Customers using POST: /api/Customers
Request body:

{

  "customerName": "Seshu",
  
  "points": 0
  
}

Add Transaction using POST: /api/Transaction
Request body:
{
  "customerId": 1,
  
  "transactionDate": "2022-10-02T04:09:14.984Z",
  
  "amount": 120
  
}

Verify Rewards using GET: /api/Rewards/{id}

Example response:
{
  "customerId": 2,
  
  "lastMonthRewardsPoints": 0,
  
  "lastSecondMonthRewardPoints": 450,
  
  "lastThridMonthRewardPoints": 0,
  
  "totalRewards": 450
  
}
