namespace Net.Core.API.Serverless.Domain.Interfaces.Request
{
    public interface ICoinRequest
    {
        string Origin { get; set; }
        string CoinName { get; set; }
        dynamic Info { get; set; }
        string CoinValue { get; set; }
    }
}
