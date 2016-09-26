// <copyright file="Ruling.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using System;
    using Newtonsoft.Json;

    public class RulingDto
    {
        [JsonIgnore]
        public DateTime? Date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.DateString))
                {
                    return null;
                }

                return DateTime.Parse(this.DateString);
            }
        }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "date")]
        private string DateString
        {
            get;
            set;
        }
    }
}