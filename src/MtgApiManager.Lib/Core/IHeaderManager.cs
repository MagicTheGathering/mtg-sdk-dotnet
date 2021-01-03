using System.Collections.Generic;

namespace MtgApiManager.Lib.Core
{
    internal interface IHeaderManager
    {
        T Get<T>(ResponseHeader responseHeader);

        void Update(IReadOnlyList<(string Name, string Value)> headers);
    }
}