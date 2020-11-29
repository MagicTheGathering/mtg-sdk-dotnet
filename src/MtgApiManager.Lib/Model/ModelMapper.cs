using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MtgApiManager.Lib.Dto;

namespace MtgApiManager.Lib.Model
{
    internal class ModelMapper : IModelMapper
    {
        public ICard MapCard(CardDto cardDto)
        {
            if (cardDto == null)
            {
                throw new ArgumentNullException(nameof(cardDto));
            }

            Uri.TryCreate(cardDto.ImageUrl, UriKind.Absolute, out var imageUri);

            return new Card
            {
                Artist = cardDto.Artist,
                Border = cardDto.Border,
                Cmc = cardDto.Cmc,
                ColorIdentity = cardDto.ColorIdentity,
                Colors = cardDto.Colors,
                Flavor = cardDto.Flavor,
                ForeignNames = cardDto.ForeignNames?.Select(x => MapForeignName(x)).ToList(),
                Hand = cardDto.Hand,
                Id = cardDto.Id,
                ImageUrl = imageUri,
                Layout = cardDto.Layout,
                Legalities = cardDto.Legalities?.Select(x => MapLegality(x)).ToList(),
                Life = cardDto.Life,
                Loyalty = cardDto.Loyalty,
                ManaCost = cardDto.ManaCost,
                MultiverseId = cardDto.MultiverseId,
                Name = cardDto.Name,
                Names = cardDto.Names,
                Number = cardDto.Number,
                OriginalText = cardDto.OriginalText,
                OriginalType = cardDto.OriginalType,
                Power = cardDto.Power,
                Printings = cardDto.Printings,
                Rarity = cardDto.Rarity,
                ReleaseDate = cardDto.ReleaseDate,
                Reserved = cardDto.Reserved,
                Rulings = cardDto.Rulings?.Select(x => MapRuling(x)).ToList(),
                Set = cardDto.Set,
                SetName = cardDto.SetName,
                Source = cardDto.Source,
                Starter = cardDto.Starter,
                SubTypes = cardDto.SubTypes,
                SuperTypes = cardDto.SuperTypes,
                Text = cardDto.Text,
                Timeshifted = cardDto.Timeshifted,
                Toughness = cardDto.Toughness,
                Type = cardDto.Type,
                Types = cardDto.Types,
                Variations = cardDto.Variations,
                Watermark = cardDto.Watermark,
            };
        }

        public IForeignName MapForeignName(ForeignNameDto foreignNameDto)
        {
            if (foreignNameDto == null)
            {
                throw new ArgumentNullException(nameof(foreignNameDto));
            }

            return new ForeignName
            {
                Language = foreignNameDto.Language,
                MultiverseId = foreignNameDto.MultiverseId,
                Name = foreignNameDto.Name,
            };
        }

        public ILegality MapLegality(LegalityDto legalityDto)
        {
            if (legalityDto == null)
            {
                throw new ArgumentNullException(nameof(legalityDto));
            }

            return new Legality
            {
                Format = legalityDto.Format,
                LegalityName = legalityDto.Legality,
            };
        }

        public IRuling MapRuling(RulingDto rulingDto)
        {
            if (rulingDto == null)
            {
                throw new ArgumentNullException(nameof(rulingDto));
            }

            return new Ruling
            {
                Date = rulingDto.Date,
                Text = rulingDto.Text,
            };
        }

        public ISet MapSet(SetDto setDto)
        {
            if (setDto == null)
            {
                throw new ArgumentNullException(nameof(setDto));
            }

            var booster = new List<object>();
            if (setDto.Booster.ValueKind == JsonValueKind.Array)
            {
                booster = setDto.Booster
                    .EnumerateArray()
                    .Select(x => GetBoosterValue(x))
                    .ToList();
            }

            return new Set
            {
                Block = setDto.Block,
                Booster = booster,
                Border = setDto.Border,
                Code = setDto.Code,
                Expansion = setDto.Expansion,
                GathererCode = setDto.GathererCode,
                MagicCardsInfoCode = setDto.MagicCardsInfoCode,
                Name = setDto.Name,
                OldCode = setDto.OldCode,
                OnlineOnly = setDto.OnlineOnly,
                ReleaseDate = setDto.ReleaseDate,
                Type = setDto.Type,
            };
        }

        private static List<object> GetBoosterAsArray(JsonElement jsonElement)
        {
            var items = new List<object>();
            foreach (var item in jsonElement.EnumerateArray())
            {
                if (item.ValueKind == JsonValueKind.String)
                {
                    items.Add(GetBoosterValue(item));
                }
                else
                {
                    items.Add(GetBoosterAsArray(item));
                }
            }

            return items;
        }

        private static object GetBoosterValue(JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.String)
            {
                return jsonElement.GetString();
            }
            else if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                return GetBoosterAsArray(jsonElement);
            }

            return null;
        }
    }
}