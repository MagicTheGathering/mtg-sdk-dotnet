using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class RootCardListDto : IMtgResponse
    {
        [JsonPropertyName("cards")]
        public List<CardDto> Cards { get; set; }
    }
}