// <copyright file="RootCardDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a the root wrapper to read a single card.
    /// </summary>
    public class RootCardDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "card")]
        public CardDto Card
        {
            get;
            set;
        }
    }
}