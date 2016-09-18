// <copyright file="CardList.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Dto.Cards
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Object representing a list of cards.
    /// </summary>
    [DataContract]
    public class CardList : MtgResponseBase
    {
        [DataMember(Name = "cards")]
        public List<Card> Cards
        {
            get;
            set;
        }
    }
}