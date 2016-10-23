// <copyright file="RootSetDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Set
{
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a root wrapper to read a single set.
    /// </summary>
    public class RootSetDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "set")]
        public SetDto Set
        {
            get;
            set;
        }
    }
}