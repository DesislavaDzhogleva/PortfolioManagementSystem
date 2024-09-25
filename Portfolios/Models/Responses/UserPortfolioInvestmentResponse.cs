namespace Portfolios.Models.Responses
{
    public class UserPortfolioInvestmentResponse
    {
        public string UserId { get; set; }

        public decimal Investment { get; set; }

        public static UserPortfolioInvestmentResponse BaseResponse(string userId)
             => new UserPortfolioInvestmentResponse()
                {
                    UserId = userId,
                    Investment = 0
                };
    }
}
