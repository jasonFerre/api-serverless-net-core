using Net.Core.API.Serverless.App.Util;
using System.Collections.Generic;

namespace Net.Core.API.Serverless.App.Interfaces
{
    public interface ICoinService
    {
        Response Add(string body);
        Response GetCoinsByOrigin(IDictionary<string, string> parameters);
        Response DeleteCoinByOrigin(IDictionary<string, string> parameters);
    }
}
