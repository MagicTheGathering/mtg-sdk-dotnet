// <copyright file="ForeignName.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ForeignName
    {
        [DataMember(Name = "language")]
        public string Language
        {
            get;
            set;
        }

        [DataMember(Name = "multiverseid")]
        public int MultiverseId
        {
            get;
            set;
        }

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }
    }
}