using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flurl;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Dto;
using MtgApiManager.Lib.Dto.Set;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Utility;

namespace MtgApiManager.Lib.Service
{
    internal class SetService : ServiceBase<ISet>, ISetService
    {
        public SetService(
            IHeaderManager headerManager,
            IModelMapper modelMapper,
            ApiVersion version,
            IRateLimit rateLimit)
            : base(headerManager, modelMapper, version, ApiEndPoint.Sets, rateLimit)
        {
        }

        /// <inheritdoc />
        public async override Task<IOperationResult<List<ISet>>> AllAsync()
        {
            try
            {
                var rootSetList = await CallWebServiceGet<RootSetListDto>(CurrentQueryUrl).ConfigureAwait(false);
                ResetCurrentUrl();

                return OperationResult<List<ISet>>.Success(MapSetsList(rootSetList), GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<ISet>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<IOperationResult<ISet>> FindAsync(string code)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, EndPoint.Name, code);
                var rootSet = await CallWebServiceGet<RootSetDto>(url).ConfigureAwait(false);
                var model = ModelMapper.MapSet(rootSet.Set);

                return OperationResult<ISet>.Success(model, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<ISet>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public async Task<IOperationResult<List<ICard>>> GenerateBoosterAsync(string code)
        {
            try
            {
                var url = BaseMtgUrl.AppendPathSegments(Version.Name, EndPoint.Name, code, "booster");
                var rootCardList = await CallWebServiceGet<RootCardListDto>(url).ConfigureAwait(false);

                var cards = rootCardList.Cards
                .Select(x => ModelMapper.MapCard(x))
                .ToList();

                return OperationResult<List<ICard>>.Success(cards, GetPagingInfo());
            }
            catch (Exception ex)
            {
                return OperationResult<List<ICard>>.Failure(ex);
            }
        }

        /// <inheritdoc />
        public void Reset() => ResetCurrentUrl();

        /// <inheritdoc />
        public ISetService Where<U>(Expression<Func<SetQueryParameter, U>> property, U value)
        {
            if (EqualityComparer<U>.Default.Equals(value, default))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(property.Body is MemberExpression expression))
            {
                throw new ArgumentNullException(nameof(property));
            }

            var queryName = QueryUtility.GetQueryPropertyName<SetQueryParameter>(expression.Member.Name);
            CurrentQueryUrl.SetQueryParam(queryName, Convert.ToString(value));

            return this;
        }

        private List<ISet> MapSetsList(RootSetListDto setListDto)
        {
            if (setListDto == null)
            {
                throw new ArgumentNullException(nameof(setListDto));
            }

            if (setListDto.Sets == null)
            {
                return new List<ISet>();
            }

            return setListDto.Sets
                .Select(x => ModelMapper.MapSet(x))
                .ToList();
        }
    }
}