using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    internal class RootCardDto : IMtgResponse
    {
        [JsonPropertyName("card")]
        public CardDto Card { get; set; }
    }
}