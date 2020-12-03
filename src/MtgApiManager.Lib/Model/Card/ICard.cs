using System;
using System.Collections.Generic;

namespace MtgApiManager.Lib.Model
{
    /// <summary>
    /// Object representing a MTG card.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Gets the artist of the card.
        /// </summary>
        string Artist { get; set; }

        /// <summary>
        /// Gets the border of the card. If the border for this specific card is DIFFERENT than the border specified in the top level set JSON, then it will be specified here. (Example: Unglued has silver borders, except for the lands which are black bordered)
        /// </summary>
        string Border { get; set; }

        /// <summary>
        /// Gets the converted Mana cost.
        /// </summary>
        float? Cmc { get; set; }

        /// <summary>
        /// Gets the card colors by color code. [“Red”, “Blue”] becomes [“R”, “U”]
        /// </summary>
        string[] ColorIdentity { get; set; }

        /// <summary>
        /// Gets the card colors. Usually this is derived from the casting cost, but some cards are special (like the back of dual sided cards and Ghostfire).
        /// </summary>
        string[] Colors { get; set; }

        /// <summary>
        /// Gets the flavor text of the card.
        /// </summary>
        string Flavor { get; set; }

        /// <summary>
        /// Gets the foreign language names for the card, if this card in this set was printed in another language. Not available for all sets.
        /// </summary>
        List<IForeignName> ForeignNames { get; set; }

        /// <summary>
        /// Gets the maximum hand size modifier. Only exists for Vanguard cards.
        /// </summary>
        int? Hand { get; set; }

        /// <summary>
        /// Gets the identifier of the card.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets the image URL for a card. Only exists if the card has a multiverse id.
        /// </summary>
        Uri ImageUrl { get; set; }

        /// <summary>
        /// Gets a value indicating whether the card has more than a single color.
        /// </summary>
        bool IsMultiColor { get; }

        /// <summary>
        /// Gets a value indicating whether the card is colorless.
        /// </summary>
        bool IsColorless { get; }

        /// <summary>
        /// Gets the card layout. Possible values: normal, split, flip, double-faced, token, plane, scheme, phenomenon, leveler, vanguard
        /// </summary>
        string Layout { get; set; }

        /// <summary>
        /// Gets which formats this card is legal, restricted or banned
        /// </summary>
        List<ILegality> Legalities { get; set; }

        /// <summary>
        /// Gets the starting life total modifier. Only exists for Vanguard cards.
        /// </summary>
        int? Life { get; set; }

        /// <summary>
        /// Gets the loyalty of the card. This is only present for planeswalkers.
        /// </summary>
        string Loyalty { get; set; }

        /// <summary>
        /// Gets the mana cost of this card. Consists of one or more Mana symbols.
        /// </summary>
        string ManaCost { get; set; }

        /// <summary>
        /// Gets the multiverse identifier of the card on Wizard’s Gatherer web page. Cards from sets that do not exist on Gatherer will NOT have a multiverse identifier. Sets not on Gatherer are: ATH, ITP, DKM, RQS, DPA and all sets with a 4 letter code that starts with a lowercase 'p’.
        /// </summary>
        int? MultiverseId { get; set; }

        /// <summary>
        /// Gets the card name. For split, double-faced and flip cards, just the name of one side of the card. Basically each ‘sub-card’ has its own record.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the names of the card. Only used for split, flip and dual cards. Will contain all the names on this card, front or back.
        /// </summary>
        string[] Names { get; set; }

        /// <summary>
        /// Gets the card number. This is printed at the bottom-center of the card in small text..
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// Gets the original text on the card at the time it was printed. This field is not available for promo cards.
        /// </summary>
        string OriginalText { get; set; }

        /// <summary>
        /// Gets the original type on the card at the time it was printed. This field is not available for promo cards.
        /// </summary>
        string OriginalType { get; set; }

        /// <summary>
        /// Gets the power of the card. This is only present for creatures.
        /// </summary>
        string Power { get; set; }

        /// <summary>
        /// Gets the sets that this card was printed in, expressed as an array of set codes.
        /// </summary>
        string[] Printings { get; set; }

        /// <summary>
        /// Gets the rarity of the card. Examples: Common, Uncommon, Rare, Mythic Rare, Special, Basic Land
        /// </summary>
        string Rarity { get; set; }

        /// <summary>
        /// Gets the date this card was released. This is only set for promo cards. The date may not be accurate to an exact day and month, thus only a partial date may be set (YYYY-MM-DD or YYYY-MM or YYYY). Some promo cards do not have a known release date.
        /// </summary>
        string ReleaseDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether this card is reserved by Wizards Official Reprint Policy.
        /// </summary>
        bool? Reserved { get; set; }

        /// <summary>
        /// Gets the rulings for the card.
        /// </summary>
        List<IRuling> Rulings { get; set; }

        /// <summary>
        /// Gets the set the card belongs to (set code).
        /// </summary>
        string Set { get; set; }

        /// <summary>
        /// Gets the set the card belongs to.
        /// </summary>
        string SetName { get; set; }

        /// <summary>
        /// Gets where this card was originally obtained for promo cards. For box sets that are theme decks, this is which theme deck the card is from.
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Gets a value indicating whether this card was only released as part of a core box set. These are technically part of the core sets and are tournament legal despite not being available in boosters.
        /// </summary>
        bool? Starter { get; set; }

        /// <summary>
        /// Gets the he subtypes of the card. These appear to the right of the dash in a card type. Usually each word is its own subtype. Example values: Trap, Arcane, Equipment, Aura, Human, Rat, Squirrel, etc.
        /// </summary>
        string[] SubTypes { get; set; }

        /// <summary>
        /// Gets the super types of the card. These appear to the far left of the card type. Example values: Basic, Legendary, Snow, World, Ongoing
        /// </summary>
        string[] SuperTypes { get; set; }

        /// <summary>
        /// Gets the oracle text of the card. May contain mana symbols and other symbols.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets the a value indicating whether this card was a time shifted card in the set.
        /// </summary>
        bool? Timeshifted { get; set; }

        /// <summary>
        /// Gets the toughness of the card. This is only present for creatures.
        /// </summary>
        string Toughness { get; set; }

        /// <summary>
        /// Gets the card type. This is the type you would see on the card if printed today. Note: The dash is a UTF8 'long dash’ as per the MTG rules
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets the types of the card. These appear to the left of the dash in a card type. Example values: Instant, Sorcery, Artifact, Creature, Enchantment, Land, Planeswalker
        /// </summary>
        string[] Types { get; set; }

        /// <summary>
        /// Gets if a card has alternate art (for example, 4 different Forests, or the 2 Brothers Yamazaki) then each other variation’s multiverseid will be listed here, NOT including the current card’s multiverseid.
        /// </summary>
        string[] Variations { get; set; }

        /// <summary>
        /// Gets the watermark on the card. Note: Split cards don’t currently have this field set, despite having a watermark on each side of the split card.
        /// </summary>
        string Watermark { get; set; }
    }
}