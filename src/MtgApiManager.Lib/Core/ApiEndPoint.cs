namespace MtgApiManager.Lib.Core
{
    internal class ApiEndPoint : Enumeration
    {
        public static readonly ApiEndPoint Cards = new ApiEndPoint(1, nameof(Cards).ToLower());
        public static readonly ApiEndPoint Formats = new ApiEndPoint(6, nameof(Formats).ToLower());
        public static readonly ApiEndPoint None = new ApiEndPoint(0, nameof(None).ToLower());
        public static readonly ApiEndPoint Sets = new ApiEndPoint(2, nameof(Sets).ToLower());
        public static readonly ApiEndPoint SubTypes = new ApiEndPoint(5, nameof(SubTypes).ToLower());
        public static readonly ApiEndPoint SuperTypes = new ApiEndPoint(4, nameof(SuperTypes).ToLower());
        public static readonly ApiEndPoint Types = new ApiEndPoint(3, nameof(Types).ToLower());

        private ApiEndPoint(int id, string name)
            : base(id, name)
        {
        }
    }
}