// <copyright file="SetQueryParameters.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Service
{
    using Newtonsoft.Json;

    /// <summary>
    /// Query parameters for the <see cref="Model.Set"/> object.
    /// </summary>
    public class SetQueryParameter : QueryParameterBase
    {
        /// <summary>
        /// Gets or sets the block the set is in.
        /// </summary>
        [JsonProperty("block")]
        public string Block
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the set.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }
    }
}