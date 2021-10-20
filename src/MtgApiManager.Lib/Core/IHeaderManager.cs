using Flurl.Util;

namespace MtgApiManager.Lib.Core
{
    internal interface IHeaderManager
    {
        T Get<T>(ResponseHeader responseHeader);

        void Update(IReadOnlyNameValueList<string> headers);
    }
}