using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class RootCardFormatsDto : IMtgResponse
    {
        [JsonPropertyName("formats")]
        public List<string> Formats { get; set; }
    }
}