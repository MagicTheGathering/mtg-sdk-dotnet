using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    /// <summary>
    /// Object representing a root wrapper to read in card super types.
    /// </summary>
    public class RootCardSuperTypeDto : MtgResponseBase
    {
        [JsonPropertyName("supertypes")]
        public List<string> SuperTypes { get; set; }
    }
}