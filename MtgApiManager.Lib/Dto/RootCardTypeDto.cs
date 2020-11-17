using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    /// <summary>
    /// Object representing a root wrapper to read in card types.
    /// </summary>
    public class RootCardTypeDto : MtgResponseBase
    {
        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }
}