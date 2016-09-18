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
        [IgnoreDataMember]
        public DateTime? Date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.DateString))
                {
                    return null;
                }

                return DateTime.Parse(this.DateString);
            }
        }

        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }

        [DataMember(Name = "date")]
        private string DateString
        {
            get;
            set;
        }
    }
}