using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class RootCardSuperTypeDto : IMtgResponse
    {
        [JsonPropertyName("supertypes")]
        public List<string> SuperTypes { get; set; }
    }
}