// <copyright file="RootCard.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
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