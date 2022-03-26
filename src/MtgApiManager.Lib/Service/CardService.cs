using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            ApiVersion version,
            IRateLimit rateLimit)
            : base(headerManager, modelMapper, version, ApiEndPoint.Cards, rateLimit)
        {
        }

        /// <inheritdoc />
        public async Task<IOperationResult<List<ICard>>> AllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var rootCardList = await CallWebServiceGet<RootCardListDto>(CurrentQueryUrl, cancellationToken).ConfigureAwait(false);
                ResetCurrentUrl();

                return OperationResult<List<ICard>>.Success(MapCardsList(rootCardList), GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<ICard>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public Task<IOperationResult<ICard>> FindAsync(int multiverseId, CancellationToken cancellationToken = default) =>
            FindAsync(multiverseId.ToString(), cancellationToken);

        /// <inheritdoc />
        public async Task<IOperationResult<ICard>> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, EndPoint.Name, id);
                var rootCard = await CallWebServiceGet<RootCardDto>(url, cancellationToken).ConfigureAwait(false);
                var model = ModelMapper.MapCard(rootCard.Card);

                return OperationResult<ICard>.Success(model, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<ICard>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<IOperationResult<List<string>>> GetCardSubTypesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.SubTypes.Name);
                var rootTypeList = await CallWebServiceGet<RootCardSubTypeDto>(url, cancellationToken).ConfigureAwait(false);

                return OperationResult<List<string>>.Success(rootTypeList.SubTypes, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<IOperationResult<List<string>>> GetCardSuperTypesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.SuperTypes.Name);
                var rootTypeList = await CallWebServiceGet<RootCardSuperTypeDto>(url, cancellationToken).ConfigureAwait(false);

                return OperationResult<List<string>>.Success(rootTypeList.SuperTypes, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<IOperationResult<List<string>>> GetCardTypesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.Types.Name);
                var rootTypeList = await CallWebServiceGet<RootCardTypeDto>(url, cancellationToken).ConfigureAwait(false);

                return OperationResult<List<string>>.Success(rootTypeList.Types, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<IOperationResult<List<string>>> GetFormatsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, ApiEndPoint.Formats.Name);
                var rootFormatsList = await CallWebServiceGet<RootCardFormatsDto>(url, cancellationToken).ConfigureAwait(false);

                return OperationResult<List<string>>.Success(rootFormatsList.Formats, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<string>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public void Reset() => ResetCurrentUrl();

        /// <inheritdoc />
        public ICardService Where<U>(Expression<Func<CardQueryParameter, U>> property, U value)
        {
            if (EqualityComparer<U>.Default.Equals(value, default))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(property.Body is MemberExpression expression))
            {
                throw new ArgumentNullException(nameof(property));
            }

            var queryName = QueryUtility.GetQueryPropertyName<CardQueryParameter>(expression.Member.Name);
            CurrentQueryUrl.SetQueryParam(queryName, Convert.ToString(value));

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