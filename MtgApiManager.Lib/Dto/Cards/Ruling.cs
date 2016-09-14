// <copyright file="Ruling.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Ruling
    {
        [DataMember(Name = "date")]
        public DateTime Date
        {
            get;
            set;
        }

        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }
    }
}