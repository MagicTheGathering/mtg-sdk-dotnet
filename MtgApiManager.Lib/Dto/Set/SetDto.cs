// <copyright file="SetDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using Newtonsoft.Json;

    public class SetDto
    {
        [JsonProperty(PropertyName = "block")]
        public string Block
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "Booster")]
        public object[] Booster
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "border")]
        public string Border
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "code")]
        public string Code
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "expansion")]
        public string Expansion
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "gathererCode")]
        public string GathererCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "magicCardsInfoCode")]
        public string MagicCardsInfoCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "oldCode")]
        public string OldCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "onlineOnly")]
        public bool? OnlineOnly
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "ReleaseDate")]
        public string ReleaseDate
        {
            get;
            set;
        }
    }
}