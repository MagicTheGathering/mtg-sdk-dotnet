// <copyright file="CardDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using System;
    using Newtonsoft.Json;

    public class CardDto
    {
        [JsonProperty(PropertyName = "artist")]
        public string Artist
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

        [JsonProperty(PropertyName = "cmc")]
        public float? Cmc
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "colorIdentity")]
        public string[] ColorIdentity
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "colors")]
        public string[] Colors
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "flavor")]
        public string Flavor
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "foreignNames")]
        public ForeignNameDto[] ForeignNames
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "hand")]
        public int? Hand
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "imageUrl")]
        public Uri ImageUrl
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "layout")]
        public string Layout
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "legalities")]
        public LegalityDto[] Legalities
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "life")]
        public int? Life
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "loyalty")]
        public string Loyalty
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "manaCost")]
        public string ManaCost
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "multiverseid")]
        public int? MultiverseId
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

        [JsonProperty(PropertyName = "names")]
        public string[] Names
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "number")]
        public string Number
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "originalText")]
        public string OriginalText
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "originalType")]
        public string OriginalType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "power")]
        public string Power
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "printings")]
        public string[] Printings
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "rarity")]
        public string Rarity
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "releaseDate")]
        public string ReleaseDate
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "reserved")]
        public bool? Reserved
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "rulings")]
        public RulingDto[] Rulings
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "set")]
        public string Set
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "setName")]
        public string SetName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "source")]
        public string Source
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "starter")]
        public bool? Starter
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "subtypes")]
        public string[] SubTypes
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "supertypes")]
        public string[] SuperTypes
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "timeshifted")]
        public bool? Timeshifted
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "toughness")]
        public string Toughness
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "types")]
        public string[] Types
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "variations")]
        public string[] Variations
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "watermark")]
        public string Watermark
        {
            get;
            set;
        }
    }
}