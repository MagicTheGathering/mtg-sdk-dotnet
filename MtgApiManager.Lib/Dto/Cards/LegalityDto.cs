// <copyright file="Ruling.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using Newtonsoft.Json;

    public class LegalityDto
    {
        [JsonProperty(PropertyName = "format")]
        public string Format
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "legality")]
        public string LegalityName
        {
            get;
            set;
        }
    }
}