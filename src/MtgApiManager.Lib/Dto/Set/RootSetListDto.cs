using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto.Set
{
    /// <summary>
    /// Object representing a the root wrapper to read a list of sets.
    /// </summary>
    public class RootSetListDto : MtgResponseBase
    {
        [JsonPropertyName("sets")]
        public List<SetDto> Sets { get; set; }
    }
}