// <copyright file="Card.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Card : MtgResponseBase
    {
        [DataMember(Name = "artist")]
        public string Artist
        {
            get;
            set;
        }

        [DataMember(Name = "border")]
        public string Border
        {
            get;
            set;
        }

        [DataMember(Name = "cmc")]
        public int? Cmc
        {
            get;
            set;
        }

        [DataMember(Name = "colors")]
        public string[] Colors
        {
            get;
            set;
        }

        [DataMember(Name = "flavor")]
        public string Flavor
        {
            get;
            set;
        }

        [DataMember(Name = "foreignNames")]
        public ForeignName[] ForeignNames
        {
            get;
            set;
        }

        [DataMember(Name = "hand")]
        public int? Hand
        {
            get;
            set;
        }

        [DataMember(Name = "id")]
        public string Id
        {
            get;
            set;
        }

        [DataMember(Name = "imageUrl")]
        public Uri ImageUrl
        {
            get;
            set;
        }

        [DataMember(Name = "layout")]
        public string Layout
        {
            get;
            set;
        }

        [DataMember(Name = "legalities")]
        public Legality[] Legalities
        {
            get;
            set;
        }

        [DataMember(Name = "life")]
        public int? Life
        {
            get;
            set;
        }

        [DataMember(Name = "loyalty")]
        public string Loyalty
        {
            get;
            set;
        }

        [DataMember(Name = "manaCost")]
        public string ManaCost
        {
            get;
            set;
        }

        [DataMember(Name = "multiverseid")]
        public int? MultiverseId
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "names")]
        public string[] Names
        {
            get;
            set;
        }

        [DataMember(Name = "number")]
        public string Number
        {
            get;
            set;
        }

        [DataMember(Name = "originalText")]
        public string OriginalText
        {
            get;
            set;
        }

        [DataMember(Name = "originalType")]
        public string OriginalType
        {
            get;
            set;
        }

        [DataMember(Name = "power")]
        public string Power
        {
            get;
            set;
        }

        [DataMember(Name = "printings")]
        public string[] Printings
        {
            get;
            set;
        }

        [DataMember(Name = "rarity")]
        public string Rarity
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public DateTime? ReleaseDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.ReleaseDateString))
                {
                    return null;
                }

                return DateTime.Parse(this.ReleaseDateString);
            }
        }

        [DataMember(Name = "reserved")]
        public bool? Reserved
        {
            get;
            set;
        }

        [DataMember(Name = "rulings")]
        public Ruling[] Rulings
        {
            get;
            set;
        }

        [DataMember(Name = "set")]
        public string Set
        {
            get;
            set;
        }

        [DataMember(Name = "setName")]
        public string SetName
        {
            get;
            set;
        }

        [DataMember(Name = "source")]
        public string Source
        {
            get;
            set;
        }

        [DataMember(Name = "starter")]
        public bool? Starter
        {
            get;
            set;
        }

        [DataMember(Name = "subtypes")]
        public string[] SubTypes
        {
            get;
            set;
        }

        [DataMember(Name = "supertypes")]
        public string[] SuperTypes
        {
            get;
            set;
        }

        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }

        [DataMember(Name = "timeshifted")]
        public bool? Timeshifted
        {
            get;
            set;
        }

        [DataMember(Name = "toughness")]
        public string Toughness
        {
            get;
            set;
        }

        [DataMember(Name = "type")]
        public string Type
        {
            get;
            set;
        }

        [DataMember(Name = "types")]
        public string[] Types
        {
            get;
            set;
        }

        [DataMember(Name = "variations")]
        public int[] Variations
        {
            get;
            set;
        }

        [DataMember(Name = "watermark")]
        public string Watermark
        {
            get;
            set;
        }

        [DataMember(Name = "releaseDate")]
        private string ReleaseDateString
        {
            get;
            set;
        }
    }
}