// <copyright file="RootCardSuperType.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a root wrapper to read in card super types.
    /// </summary>
    public class RootCardSuperTypeDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "supertypes")]
        public List<string> SuperTypes
        {
            get;
            set;
        }
    }
}