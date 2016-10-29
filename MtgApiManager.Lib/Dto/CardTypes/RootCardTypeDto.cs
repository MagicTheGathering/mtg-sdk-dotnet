// <copyright file="RootCardType.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a root wrapper to read in card types.
    /// </summary>
    public class RootCardTypeDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "types")]
        public List<string> Types
        {
            get;
            set;
        }
    }
}