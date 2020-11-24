namespace MtgApiManager.Lib.Dto
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Object representing a the root wrapper to read a list of cards.
    /// </summary>
    public class RootCardListDto : MtgResponseBase
    {
        [JsonPropertyName("cards")]
        public List<CardDto> Cards { get; set; }
    }
}