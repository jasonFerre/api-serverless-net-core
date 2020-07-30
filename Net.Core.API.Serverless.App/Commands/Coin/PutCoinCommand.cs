using Net.Core.API.Serverless.Domain.Interfaces.Request;

namespace Net.Core.API.Serverless.App.Commands.Coin
{
    public class PutCoinCommand : ICoinRequest
    {
        public string Origin { get; set; }
        public string CoinName { get; set; }
        public dynamic Info { get; set; }
        public string CoinValue { get; set; }
    }
}
