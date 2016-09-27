// <copyright file="Card.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Test.Utility
{
    using Newtonsoft.Json;

    public class DtoTestObject
    {
        public string Property1
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "jsonProperty2")]
        public string Property2
        {
            get;
            set;
        }
    }
}