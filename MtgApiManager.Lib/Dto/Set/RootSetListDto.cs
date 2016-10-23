// <copyright file="RootSetListDto.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Set
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Object representing a the root wrapper to read a list of sets.
    /// </summary>
    public class RootSetListDto : MtgResponseBase
    {
        [JsonProperty(PropertyName = "sets")]
        public List<SetDto> Sets
        {
            get;
            set;
        }
    }
}