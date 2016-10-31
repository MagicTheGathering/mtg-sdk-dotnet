// <copyright file="RootCardSubType.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a root wrapper to read in card sub types.
    /// </summary>
    public class RootCardSubTypeDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "subtypes")]
        public List<string> SubTypes
        {
            get;
            set;
        }
    }
}