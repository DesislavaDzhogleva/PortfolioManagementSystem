namespace Portfolios.Models.Responses
{
    public class UserPortfolioInvestmentResponse
    {
        public string UserId { get; set; }

        public decimal Investment { get; set; }

        public bool Exists { get; set; }

        public static UserPortfolioInvestmentResponse BaseResponse(string userId)
             => new UserPortfolioInvestmentResponse()
                {
                     UserId = userId,
                     Exists = true,
                     Investment = 0
                };

        public static UserPortfolioInvestmentResponse EmptyResponse(string userId)
            => new UserPortfolioInvestmentResponse()
            {
                UserId = userId,
                Exists = false,
                Investment = 0
            };
    }
}
