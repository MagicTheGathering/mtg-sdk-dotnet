using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class RootCardTypeDto : IMtgResponse
    {
        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }
}