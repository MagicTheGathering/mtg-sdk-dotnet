// <copyright file="ForeignNameDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using Newtonsoft.Json;

    public class ForeignNameDto
    {
        [JsonProperty(PropertyName = "language")]
        public string Language
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
    }
}