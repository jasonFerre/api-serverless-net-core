using Net.Core.API.Serverless.Domain.Extensions;
using Net.Core.API.Serverless.Domain.Interfaces.Request;
namespace Net.Core.API.Serverless.Domain.Entities
{
    public class Coin
    {
        public string Origin { get; set; }
        public string CoinName { get; set; }
        public dynamic Info { get; set; }
        public string CoinValue { get; set; }

        public Coin(ICoinRequest request)
        {
            Validation(request);
            Origin = request.Origin;
            CoinName = request.CoinName;
            Info = request.Info;
            CoinValue = request.CoinValue;
        }

        public Coin() {}

        public void Validation(ICoinRequest request)
        {
            if (string.IsNullOrEmpty(request.CoinName) || string.IsNullOrWhiteSpace(request.CoinName))
                throw new DomainException($"The request parameter CoinName is null or empty, this field is required");
            if (string.IsNullOrEmpty(request.Origin) || string.IsNullOrWhiteSpace(request.Origin))
                throw new DomainException($"The request parameter CoinName is null or empty, this field is required");
        }
    }
}
