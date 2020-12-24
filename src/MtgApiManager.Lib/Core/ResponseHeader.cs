namespace MtgApiManager.Lib.Core
{
    internal class ResponseHeader : Enumeration
    {
        public static readonly ResponseHeader Count = new ResponseHeader(0, "Count");
        public static readonly ResponseHeader Link = new ResponseHeader(1, "Link");
        public static readonly ResponseHeader PageSize = new ResponseHeader(2, "Page-Size");
        public static readonly ResponseHeader RatelimitLimit = new ResponseHeader(3, "Ratelimit-Limit");
        public static readonly ResponseHeader RatelimitRemaining = new ResponseHeader(4, "Ratelimit-Remaining");
        public static readonly ResponseHeader TotalCount = new ResponseHeader(5, "Total-Count");

        public ResponseHeader(int id, string name)
            : base(id, name)
        {
        }
    }
}