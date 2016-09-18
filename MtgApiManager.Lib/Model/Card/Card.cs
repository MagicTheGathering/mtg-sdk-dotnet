// <copyright file="Card.cs" company="Team7 Productions">
//     Copyright (c) 2014. All rights reserved.
// </copyright>
// <author>Jason Regnier</author>
namespace MtgApiManager.Lib.Model.Card
{
    using System;

    /// <summary>
    /// Object representing a mtg card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets the artist of the card.
        /// </summary>
        public string Artist
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the border of the card. If the border for this specific card is DIFFERENT than the border specified in the top level set JSON, then it will be specified here. (Example: Unglued has silver borders, except for the lands which are black bordered)
        /// </summary>
        public string Border
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the converted mana cost.
        /// </summary>
        public int? Cmc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the card colors. Usually this is derived from the casting cost, but some cards are special (like the back of dual sided cards and Ghostfire).
        /// </summary>
        public string[] Colors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the flavor text of the card.
        /// </summary>
        public string Flavor
        {
            get;
            set;
        }

        /// <summary>
        /// Get or sets the foreign language names for the card, if this card in this set was printed in another language. Not available for all sets.
        /// </summary>
        public ForeignName[] ForeignNames
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum hand size modifier. Only exists for Vanguard cards.
        /// </summary>
        public int? Hand
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the identifier of the card.
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image URL for a card. Only exists if the card has a multiverse id.
        /// </summary>
        public Uri ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the card layout. Possible values: normal, split, flip, double-faced, token, plane, scheme, phenomenon, leveler, vanguard
        /// </summary>
        public string Layout
        {
            get;
            set;
        }

        /// <summary>
        /// Get or sets which formats this card is legal, restricted or banned
        /// </summary>
        public Legality[] Legalities
        {
            get;
            set;
        }

        /// <summary>
        /// Get or sets the starting life total modifier. Only exists for Vanguard cards.
        /// </summary>
        public int? Life
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the loyalty of the card. This is only present for planeswalkers.
        /// </summary>
        public string Loyalty
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the mana cost of this card. Consists of one or more mana symbols.
        /// </summary>
        public string ManaCost
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the multiverse identifier of the card on Wizard’s Gatherer web page. Cards from sets that do not exist on Gatherer will NOT have a multiverse identifier. Sets not on Gatherer are: ATH, ITP, DKM, RQS, DPA and all sets with a 4 letter code that starts with a lowercase 'p’.
        /// </summary>
        public int? MultiverseId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the card name. For split, double-faced and flip cards, just the name of one side of the card. Basically each ‘sub-card’ has its own record.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the names of the card. Only used for split, flip and dual cards. Will contain all the names on this card, front or back.
        /// </summary>
        public string[] Names
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the card number. This is printed at the bottom-center of the card in small text..
        /// </summary>
        public string Number
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the original text on the card at the time it was printed. This field is not available for promo cards.
        /// </summary>
        public string OriginalText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the original type on the card at the time it was printed. This field is not available for promo cards.
        /// </summary>
        public string OriginalType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the power of the card. This is only present for creatures.
        /// </summary>
        public string Power
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sets that this card was printed in, expressed as an array of set codes.
        /// </summary>
        public string[] Printings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rarity of the card. Examples: Common, Uncommon, Rare, Mythic Rare, Special, Basic Land
        /// </summary>
        public string Rarity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date this card was released. This is only set for promo cards. The date may not be accurate to an exact day and month, thus only a partial date may be set (YYYY-MM-DD or YYYY-MM or YYYY). Some promo cards do not have a known release date.
        /// </summary>
        public DateTime? ReleaseDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this card is reserved by Wizards Official Reprint Policy.
        /// </summary>
        public bool? Reserved
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rulings for the card.
        /// </summary>
        public Ruling[] Rulings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set the card belongs to (set code).
        /// </summary>
        public string Set
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the set the card belongs to.
        /// </summary>
        public string SetName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets where this card was originally obtained for promo cards. For box sets that are theme decks, this is which theme deck the card is from.
        /// </summary>
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this card was only released as part of a core box set. These are technically part of the core sets and are tournament legal despite not being available in boosters.
        /// </summary>
        public bool? Starter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the he subtypes of the card. These appear to the right of the dash in a card type. Usually each word is its own subtype. Example values: Trap, Arcane, Equipment, Aura, Human, Rat, Squirrel, etc.
        /// </summary>
        public string[] SubTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the super types of the card. These appear to the far left of the card type. Example values: Basic, Legendary, Snow, World, Ongoing
        /// </summary>
        public string[] SuperTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the oracle text of the card. May contain mana symbols and other symbols.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the a value indicating whether this card was a time shifted card in the set.
        /// </summary>
        public bool? Timeshifted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the toughness of the card. This is only present for creatures.
        /// </summary>
        public string Toughness
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the card type. This is the type you would see on the card if printed today. Note: The dash is a UTF8 'long dash’ as per the MTG rules
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the types of the card. These appear to the left of the dash in a card type. Example values: Instant, Sorcery, Artifact, Creature, Enchantment, Land, Planeswalker
        /// </summary>
        public string[] Types
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if a card has alternate art (for example, 4 different Forests, or the 2 Brothers Yamazaki) then each other variation’s multiverseid will be listed here, NOT including the current card’s multiverseid.
        /// </summary>
        public int[] Variations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the watermark on the card. Note: Split cards don’t currently have this field set, despite having a watermark on each side of the split card.
        /// </summary>
        public string Watermark
        {
            get;
            set;
        }
    }
}