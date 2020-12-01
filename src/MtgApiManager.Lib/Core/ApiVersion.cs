namespace MtgApiManager.Lib.Core
{
    internal class ApiVersion : Enumeration
    {
        public static readonly ApiVersion None = new ApiVersion(0, nameof(None).ToLower());
        public static readonly ApiVersion V1 = new ApiVersion(1, nameof(V1).ToLower());

        private ApiVersion(int id, string name)
            : base(id, name)
        {
        }
    }
}