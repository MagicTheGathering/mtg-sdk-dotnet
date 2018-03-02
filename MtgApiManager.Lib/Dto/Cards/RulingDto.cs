// <copyright file="RulingDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using Newtonsoft.Json;

    public class RulingDto
    {
        [JsonProperty(PropertyName = "date")]
        public string Date
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
    }
}