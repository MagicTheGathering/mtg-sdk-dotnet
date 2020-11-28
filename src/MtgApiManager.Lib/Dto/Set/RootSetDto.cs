using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto.Set
{
    internal class RootSetDto : IMtgResponse
    {
        [JsonPropertyName("set")]
        public SetDto Set { get; set; }
    }
}