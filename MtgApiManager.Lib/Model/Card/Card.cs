// <copyright file="Card.cs">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dto;

    /// <summary>
    /// Object representing a MTG card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="item">The card data transfer object to map to.</param>
        public Card(CardDto item)
        {
            MapCard(item);
        }

        /// <summary>
        /// Gets the artist of the card.
        /// </summary>
        public string Artist
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the border of the card. If the border for this specific card is DIFFERENT than the border specified in the top level set JSON, then it will be specified here. (Example: Unglued has silver borders, except for the lands which are black bordered)
        /// </summary>
        public string Border
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the converted Mana cost.
        /// </summary>
        public float? Cmc
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the card colors by color code. [“Red”, “Blue”] becomes [“R”, “U”]
        /// </summary>
        public string[] ColorIdentity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the card colors. Usually this is derived from the casting cost, but some cards are special (like the back of dual sided cards and Ghostfire).
        /// </summary>
        public string[] Colors
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the flavor text of the card.
        /// </summary>
        public string Flavor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the foreign language names for the card, if this card in this set was printed in another language. Not available for all sets.
        /// </summary>
        public List<ForeignName> ForeignNames
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the maximum hand size modifier. Only exists for Vanguard cards.
        /// </summary>
        public int? Hand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the identifier of the card.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the image URL for a card. Only exists if the card has a multiverse id.
        /// </summary>
        public Uri ImageUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the card layout. Possible values: normal, split, flip, double-faced, token, plane, scheme, phenomenon, leveler, vanguard
        /// </summary>
        public string Layout
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets which formats this card is legal, restricted or banned
        /// </summary>
        public List<Legality> Legalities
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the starting life total modifier. Only exists for Vanguard cards.
        /// </summary>
        public int? Life
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the loyalty of the card. This is only present for planeswalkers.
        /// </summary>
        public string Loyalty
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the mana cost of this card. Consists of one or more Mana symbols.
        /// </summary>
        public string ManaCost
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the multiverse identifier of the card on Wizard’s Gatherer web page. Cards from sets that do not exist on Gatherer will NOT have a multiverse identifier. Sets not on Gatherer are: ATH, ITP, DKM, RQS, DPA and all sets with a 4 letter code that starts with a lowercase 'p’.
        /// </summary>
        public int? MultiverseId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the card name. For split, double-faced and flip cards, just the name of one side of the card. Basically each ‘sub-card’ has its own record.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the names of the card. Only used for split, flip and dual cards. Will contain all the names on this card, front or back.
        /// </summary>
        public string[] Names
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the card number. This is printed at the bottom-center of the card in small text..
        /// </summary>
        public string Number
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original text on the card at the time it was printed. This field is not available for promo cards.
        /// </summary>
        public string OriginalText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the original type on the card at the time it was printed. This field is not available for promo cards.
        /// </summary>
        public string OriginalType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the power of the card. This is only present for creatures.
        /// </summary>
        public string Power
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the sets that this card was printed in, expressed as an array of set codes.
        /// </summary>
        public string[] Printings
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rarity of the card. Examples: Common, Uncommon, Rare, Mythic Rare, Special, Basic Land
        /// </summary>
        public string Rarity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date this card was released. This is only set for promo cards. The date may not be accurate to an exact day and month, thus only a partial date may be set (YYYY-MM-DD or YYYY-MM or YYYY). Some promo cards do not have a known release date.
        /// </summary>
        public string ReleaseDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this card is reserved by Wizards Official Reprint Policy.
        /// </summary>
        public bool? Reserved
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rulings for the card.
        /// </summary>
        public List<Ruling> Rulings
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the set the card belongs to (set code).
        /// </summary>
        public string Set
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the set the card belongs to.
        /// </summary>
        public string SetName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets where this card was originally obtained for promo cards. For box sets that are theme decks, this is which theme deck the card is from.
        /// </summary>
        public string Source
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this card was only released as part of a core box set. These are technically part of the core sets and are tournament legal despite not being available in boosters.
        /// </summary>
        public bool? Starter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the he subtypes of the card. These appear to the right of the dash in a card type. Usually each word is its own subtype. Example values: Trap, Arcane, Equipment, Aura, Human, Rat, Squirrel, etc.
        /// </summary>
        public string[] SubTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the super types of the card. These appear to the far left of the card type. Example values: Basic, Legendary, Snow, World, Ongoing
        /// </summary>
        public string[] SuperTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the oracle text of the card. May contain mana symbols and other symbols.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the a value indicating whether this card was a time shifted card in the set.
        /// </summary>
        public bool? Timeshifted
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the toughness of the card. This is only present for creatures.
        /// </summary>
        public string Toughness
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the card type. This is the type you would see on the card if printed today. Note: The dash is a UTF8 'long dash’ as per the MTG rules
        /// </summary>
        public string Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the types of the card. These appear to the left of the dash in a card type. Example values: Instant, Sorcery, Artifact, Creature, Enchantment, Land, Planeswalker
        /// </summary>
        public string[] Types
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if a card has alternate art (for example, 4 different Forests, or the 2 Brothers Yamazaki) then each other variation’s multiverseid will be listed here, NOT including the current card’s multiverseid.
        /// </summary>
        public string[] Variations
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the watermark on the card. Note: Split cards don’t currently have this field set, despite having a watermark on each side of the split card.
        /// </summary>
        public string Watermark
        {
            get;
            private set;
        }

        /// <summary>
        /// Maps a single card DTO to the card model.
        /// </summary>
        /// <param name="item">The card DTO object.</param>
        private void MapCard(CardDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Artist = item.Artist;
            Border = item.Border;
            Cmc = item.Cmc;
            ColorIdentity = item.ColorIdentity;
            Colors = item.Colors;
            Flavor = item.Flavor;
            if (item.ForeignNames != null)
            {
                ForeignNames = item.ForeignNames
                        .Select(x => new ForeignName(x)).ToList();
            }

            Hand = item.Hand;
            Id = item.Id;
            ImageUrl = item.ImageUrl;
            Layout = item.Layout;
            if (item.Legalities != null)
            {
                Legalities = item.Legalities
                        .Select(x => new Legality(x)).ToList();
            }

            Life = item.Life;
            Loyalty = item.Loyalty;
            ManaCost = item.ManaCost;
            MultiverseId = item.MultiverseId;
            Name = item.Name;
            Names = item.Names;
            Number = item.Number;
            OriginalText = item.OriginalText;
            OriginalType = item.OriginalType;
            Power = item.Power;
            Printings = item.Printings;
            Rarity = item.Rarity;
            ReleaseDate = item.ReleaseDate;
            Reserved = item.Reserved;
            if (item.Rulings != null)
            {
                Rulings = item.Rulings
                      .Select(x => new Ruling(x)).ToList();
            }

            Set = item.Set;
            SetName = item.SetName;
            Source = item.Source;
            Starter = item.Starter;
            SubTypes = item.SubTypes;
            SuperTypes = item.SuperTypes;
            Text = item.Text;
            Timeshifted = item.Timeshifted;
            Toughness = item.Toughness;
            Type = item.Type;
            Types = item.Types;
            Variations = item.Variations;
            Watermark = item.Watermark;
        }
    }
}