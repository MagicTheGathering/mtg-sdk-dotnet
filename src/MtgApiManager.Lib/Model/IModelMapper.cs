using MtgApiManager.Lib.Dto;

namespace MtgApiManager.Lib.Model
{
    public interface IModelMapper
    {
        Card MapCard(CardDto cardDto);

        Set MapSet(SetDto setDto);

        ForeignName MapForeignName(ForeignNameDto foreignNameDto);

        Legality MapLegality(LegalityDto legalityDto);

        Ruling MapRuling(RulingDto rulingDto);
    }
}