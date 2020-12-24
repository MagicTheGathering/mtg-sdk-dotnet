using System.Net.Http.Headers;

namespace MtgApiManager.Lib.Core
{
    internal interface IHeaderManager
    {
        T Get<T>(ResponseHeader responseHeader);

        void Update(HttpResponseHeaders headers);
    }
}