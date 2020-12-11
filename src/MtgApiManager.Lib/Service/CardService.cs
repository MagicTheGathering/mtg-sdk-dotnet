using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Utility;

using Flurl;

namespace MtgApiManager.Lib.Service
{
    internal class CardService : ServiceBase<ICard>, ICardService
    {
        public CardService(
            IMtgApiServiceAdapter serviceAdapter,
            IModelMapper modelMapper,
            ApiVersion version,
            bool rateLimitOn = true)
            : base(serviceAdapter, modelMapper, version, ApiEndPoint.Cards, rateLimitOn)
        {
        }

        /// <inheritdoc />
        public async override Task<Exceptional<List<ICard>>> AllAsync()
        {
            try
            {
                var rootCardList = await CallWebServiceGet<RootCardListDto>(CurrentQueryUrl.ToUri()).ConfigureAwait(false);
                ResetCurrentUrl();

                return Exceptional<List<ICard>>.Success(MapCardsList(rootCardList), MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<ICard>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public Task<Exceptional<ICard>> FindAsync(int multiverseId) => FindAsync(multiverseId.ToString());

        /// <inheritdoc />
        public async Task<Exceptional<ICard>> FindAsync(string id)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, EndPoint.Name, id);
                var rootCard = await CallWebServiceGet<RootCardDto>(url.ToUri()).ConfigureAwait(false);
                var model = ModelMapper.MapCard(rootCard.Card);

                return Exceptional<ICard>.Success(model, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<ICard>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<Exceptional<List<string>>> GetCardSubTypesAsync()
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.SubTypes.Name);
                var rootTypeList = await CallWebServiceGet<RootCardSubTypeDto>(url.ToUri()).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.SubTypes, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<Exceptional<List<string>>> GetCardSuperTypesAsync()
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.SuperTypes.Name);
                var rootTypeList = await CallWebServiceGet<RootCardSuperTypeDto>(url.ToUri()).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.SuperTypes, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<Exceptional<List<string>>> GetCardTypesAsync()
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.Types.Name);
                var rootTypeList = await CallWebServiceGet<RootCardTypeDto>(url.ToUri()).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootTypeList.Types, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<Exceptional<List<string>>> GetFormatsAsync()
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.Formats.Name);
                var rootFormatsList = await CallWebServiceGet<RootCardFormatsDto>(url.ToUri()).ConfigureAwait(false);

                return Exceptional<List<string>>.Success(rootFormatsList.Formats, MtgApiController.CreatePagingInfo());
            }
            catch (Exception ex)
            {
                return Exceptional<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public void Reset() => ResetCurrentUrl();

        /// <inheritdoc />
        public ICardService Where<U>(Expression<Func<CardQueryParameter, U>> property, U value)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (EqualityComparer<U>.Default.Equals(value, default))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var expression = property.Body as MemberExpression;
            var queryName = QueryUtility.GetQueryPropertyName<CardQueryParameter>(expression.Member.Name);

            var valueType = value.GetType();
            if (valueType.IsArray)
            {
                CurrentQueryUrl.SetQueryParam(queryName, string.Join("|", (IEnumerable<object>)value));
            }
            else
            {
                CurrentQueryUrl.SetQueryParam(queryName, Convert.ToString(value));
            }

            return this;
        }

        private List<ICard> MapCardsList(RootCardListDto cardListDto)
        {
            if (cardListDto == null)
            {
                throw new ArgumentNullException(nameof(cardListDto));
            }

            if (cardListDto.Cards == null)
            {
                return new List<ICard>();
            }

            return cardListDto.Cards
                .Select(x => ModelMapper.MapCard(x))
                .ToList();
        }
    }
}