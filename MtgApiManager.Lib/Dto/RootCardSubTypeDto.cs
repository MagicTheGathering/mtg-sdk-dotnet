using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    /// <summary>
    /// Object representing a root wrapper to read in card sub types.
    /// </summary>
    public class RootCardSubTypeDto : MtgResponseBase
    {
        [JsonPropertyName("subtypes")]
        public List<string> SubTypes { get; set; }
    }
}