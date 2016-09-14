// <copyright file="Ruling.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Legality
    {
        [DataMember(Name = "format")]
        public string Format
        {
            get;
            set;
        }

        [DataMember(Name = "legality")]
        public string LegalityName
        {
            get;
            set;
        }
    }
}