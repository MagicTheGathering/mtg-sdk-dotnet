// <copyright file="LegalityDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
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