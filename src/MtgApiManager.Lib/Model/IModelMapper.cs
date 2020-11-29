using MtgApiManager.Lib.Dto;

namespace MtgApiManager.Lib.Model
{
    internal interface IModelMapper
    {
        ICard MapCard(CardDto cardDto);

        IForeignName MapForeignName(ForeignNameDto foreignNameDto);

        ILegality MapLegality(LegalityDto legalityDto);

        IRuling MapRuling(RulingDto rulingDto);

        ISet MapSet(SetDto setDto);
    }
}