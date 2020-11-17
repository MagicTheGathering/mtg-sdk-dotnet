using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto.Set
{
    /// <summary>
    /// Object representing a root wrapper to read a single set.
    /// </summary>
    public class RootSetDto : MtgResponseBase
    {
        [JsonPropertyName("set")]
        public SetDto Set { get; set; }
    }
}