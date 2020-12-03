namespace MtgApiManager.Lib.Core
{
    internal class MtgApiError : Enumeration
    {
        public static readonly MtgApiError BadRequest = new MtgApiError(400, nameof(BadRequest), "Your request is not valid");
        public static readonly MtgApiError Forbidden = new MtgApiError(403, nameof(Forbidden), "You have exceeded the rate limit");
        public static readonly MtgApiError InternalServerError = new MtgApiError(500, nameof(InternalServerError), "We had a problem with our server. Try again later");
        public static readonly MtgApiError None = new MtgApiError(0, nameof(None), string.Empty);
        public static readonly MtgApiError NotFound = new MtgApiError(404, nameof(NotFound), "The specified resource could not be found");
        public static readonly MtgApiError ServiceUnavailable = new MtgApiError(503, nameof(ServiceUnavailable), "We’re temporarily off line for maintenance. Please try again later.");

        private MtgApiError(int id, string name, string description)
            : base(id, name)
        {
            Description = description;
        }

        public string Description { get; }
    }
}