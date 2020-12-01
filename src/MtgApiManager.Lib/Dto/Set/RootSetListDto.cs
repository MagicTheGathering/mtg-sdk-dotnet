using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto.Set
{
    internal class RootSetListDto : IMtgResponse
    {
        [JsonPropertyName("sets")]
        public List<SetDto> Sets { get; set; }
    }
}