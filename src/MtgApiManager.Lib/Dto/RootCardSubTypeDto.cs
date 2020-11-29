using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class RootCardSubTypeDto : IMtgResponse
    {
        [JsonPropertyName("subtypes")]
        public List<string> SubTypes { get; set; }
    }
}