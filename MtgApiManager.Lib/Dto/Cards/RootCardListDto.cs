// <copyright file="RootCardListDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a the root wrapper to read a list of cards.
    /// </summary>
    public class RootCardListDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "cards")]
        public List<CardDto> Cards
        {
            get;
            set;
        }
    }
}