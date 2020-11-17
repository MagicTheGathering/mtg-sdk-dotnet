using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Dto
{
    /// <summary>
    /// Object representing a the root wrapper to read a single card.
    /// </summary>
    public class RootCardDto : MtgResponseBase
    {
        [JsonPropertyName("card")]
        public CardDto Card { get; set; }
    }
}